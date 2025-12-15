using demoNEW_EFCoreVersion.DatabaseModel;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace demoNEW_EFCoreVersion
{
    internal class ExcelImportData
    {
        private readonly ApplicationContext _context;

        // Словари для кэширования уже загруженных/созданных сущностей
        private Dictionary<string, UsersRole> _roleDict;
        private Dictionary<string, Suppliers> _supplierDict;
        private Dictionary<string, Manufacturers> _manufacturerDict;
        private Dictionary<string, ProductCategory> _categoryDict;
        private Dictionary<string, OrderStatus> _statusDict;
        private Units _defaultUnit;

        public ExcelImportData(ApplicationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            ExcelPackage.License.SetNonCommercialPersonal("sadasdfasdgdrafhbrtshrthbtfghfgcgf");

            // Загружаем существующие данные из БД для кэширования
            _roleDict = _context.UsersRoles.ToDictionary(r => r.Name, r => r);
            _supplierDict = _context.Suppliers.ToDictionary(s => s.Name, s => s);
            _manufacturerDict = _context.Manufacturers.ToDictionary(m => m.Name, m => m);
            _categoryDict = _context.ProductCategories.ToDictionary(c => c.Name, c => c);
            _statusDict = _context.OrderStatuses.ToDictionary(s => s.Name, s => s);
            _defaultUnit = _context.Units.FirstOrDefault(u => u.Name == "шт.");
        }

        public void ImportAllData()
        {
            // Выполняем импорт в правильном порядке зависимостей
            ImportUsersAndRoles();
            ImportPickupPoints();
            ImportProducts();
            ImportOrdersAndOrderContents();
        }

        private void ImportUsersAndRoles()
        {
            using (var package = new ExcelPackage(new FileInfo("E:\\Всё\\все остальное\\demoEX\\demoEFCoreVersion\\Data\\user_import.xlsx")))
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault() ?? throw new InvalidOperationException("Файл user_import.xlsx не содержит листов.");

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    string roleName = worksheet.Cells[row, 1].Value?.ToString().Trim();
                    string fio = worksheet.Cells[row, 2].Value?.ToString().Trim();
                    string login = worksheet.Cells[row, 3].Value?.ToString().Trim();
                    string password = worksheet.Cells[row, 4].Value?.ToString().Trim();

                    if (string.IsNullOrEmpty(roleName) || string.IsNullOrEmpty(fio)) continue;

                    // Паттерн "Найти или Создать" для Роли
                    if (!_roleDict.TryGetValue(roleName, out var role))
                    {
                        role = new UsersRole { Name = roleName };
                        _context.UsersRoles.Add(role);
                        _roleDict[roleName] = role; // Добавляем новую роль в кэш
                    }

                    var fioParts = fio.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    var user = new Users
                    {
                        LastName = fioParts.Length > 0 ? fioParts[0] : "",
                        FirstName = fioParts.Length > 1 ? fioParts[1] : "",
                        Patronymic = fioParts.Length > 2 ? fioParts[2] : "",
                        Login = login,
                        Password = password,
                        UsersRole = role
                    };
                    _context.Users.Add(user);
                }
            }
            _context.SaveChanges();
        }

        private void ImportPickupPoints()
        {
            using (var package = new ExcelPackage(new FileInfo("E:\\Всё\\все остальное\\demoEX\\demoEFCoreVersion\\Data\\Пункты выдачи_import.xlsx")))
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault() ?? throw new InvalidOperationException("Файл Пункты выдачи_import.xlsx не содержит листов.");

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    if (worksheet.Cells[row, 1].Value == null) continue;
                    int id = Convert.ToInt32(worksheet.Cells[row, 1].Value);

                    if (_context.PickupPoints.Find(id) == null)
                    {
                        _context.PickupPoints.Add(new PickupPoint
                        {
                            Id = id, // Явно указываем ID из Excel
                            Index = worksheet.Cells[row, 2].Value?.ToString().Trim(),
                            City = worksheet.Cells[row, 3].Value?.ToString().Trim(),
                            Street = worksheet.Cells[row, 4].Value?.ToString().Trim(),
                            House = worksheet.Cells[row, 5].Value?.ToString().Trim()
                        });
                    }
                }
            }

            // Используем транзакцию для безопасной вставки с явным ID
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.PickupPoints ON;");
                    _context.SaveChanges();
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.PickupPoints OFF;");
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private void ImportProducts()
        {
            // "Найти или Создать" для единицы измерения "шт."
            if (_defaultUnit == null)
            {
                _defaultUnit = new Units { Name = "шт." };
                _context.Units.Add(_defaultUnit);
            }

            using (var package = new ExcelPackage(new FileInfo("E:\\Всё\\все остальное\\demoEX\\demoEFCoreVersion\\Data\\Tovar.xlsx")))
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault() ?? throw new InvalidOperationException("Файл Tovar.xlsx не содержит листов.");

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    string article = worksheet.Cells[row, 1].Value?.ToString().Trim();
                    if (string.IsNullOrEmpty(article) || _context.Products.Find(article) != null) continue;

                    string supplierName = worksheet.Cells[row, 5].Value?.ToString().Trim();
                    string manufacturerName = worksheet.Cells[row, 6].Value?.ToString().Trim();
                    string categoryName = worksheet.Cells[row, 7].Value?.ToString().Trim();

                    // "Найти или Создать" для связанных сущностей
                    if (!_supplierDict.TryGetValue(supplierName, out var supplier))
                    {
                        supplier = new Suppliers { Name = supplierName };
                        _context.Suppliers.Add(supplier);
                        _supplierDict[supplierName] = supplier;
                    }

                    if (!_manufacturerDict.TryGetValue(manufacturerName, out var manufacturer))
                    {
                        manufacturer = new Manufacturers { Name = manufacturerName };
                        _context.Manufacturers.Add(manufacturer);
                        _manufacturerDict[manufacturerName] = manufacturer;
                    }

                    if (!_categoryDict.TryGetValue(categoryName, out var category))
                    {
                        category = new ProductCategory { Name = categoryName };
                        _context.ProductCategories.Add(category);
                        _categoryDict[categoryName] = category;
                    }

                    var product = new Product
                    {
                        Article = article,
                        Name = worksheet.Cells[row, 2].Value?.ToString().Trim(),
                        Price = float.TryParse(worksheet.Cells[row, 4].Value?.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out float p) ? p : 0,
                        Discount = float.TryParse(worksheet.Cells[row, 8].Value?.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out float d) ? d : 0,
                        QuantityInStorage = float.TryParse(worksheet.Cells[row, 9].Value?.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out float q) ? q : 0,
                        Description = worksheet.Cells[row, 10].Value?.ToString().Trim(),
                        Photo = worksheet.Cells[row, 11].Value?.ToString().Trim(),

                        
                        Units = _defaultUnit,
                        Suppliers = supplier,
                        Manufacturers = manufacturer,
                        ProductCategory = category
                    };
                    _context.Products.Add(product);
                }
            }
            _context.SaveChanges();
        }

        private void ImportOrdersAndOrderContents()
        {
            using (var package = new ExcelPackage(new FileInfo("E:\\Всё\\все остальное\\demoEX\\demoEFCoreVersion\\Data\\Заказ_import.xlsx")))
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault() ?? throw new InvalidOperationException("Файл Заказ_import.xlsx не содержит листов.");

                // Используем одну транзакцию для всех заказов
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Orders ON;");

                        for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                        {
                            if (worksheet.Cells[row, 1].Value == null) continue;
                            int orderId = Convert.ToInt32(worksheet.Cells[row, 1].Value);

                            if (_context.Orders.Find(orderId) != null) continue;

                            string statusName = worksheet.Cells[row, 8].Value?.ToString().Trim();
                            string fio = worksheet.Cells[row, 6].Value?.ToString().Trim();
                            int pickupPointId = Convert.ToInt32(worksheet.Cells[row, 5].Value);

                            // "Найти или Создать" для Статуса
                            if (!_statusDict.TryGetValue(statusName, out var status))
                            {
                                status = new OrderStatus { Name = statusName };
                                _context.OrderStatuses.Add(status);
                                _statusDict[statusName] = status;
                            }

                            // Находим связанные сущности
                            var fioParts = fio.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            var user = _context.Users.FirstOrDefault(u => u.LastName == fioParts[0] && u.FirstName == fioParts[1] && u.Patronymic == fioParts[2]);
                            var pickupPoint = _context.PickupPoints.Find(pickupPointId);

                            if (user == null || pickupPoint == null)
                            {
                                Console.WriteLine($"Не удалось найти пользователя или пункт выдачи для заказа {orderId}. Пропущено.");
                                continue;
                            }

                            var order = new Order
                            {
                                Id = orderId, // Явно указываем ID
                                DateOrder = ParseExcelDate(worksheet.Cells[row, 3]),
                                DateDelivery = ParseExcelDate(worksheet.Cells[row, 4]),
                                ReceiptCode = worksheet.Cells[row, 7].Value?.ToString().Trim(),

                                
                                OrderStatus = status,
                                Users = user,
                                PickupPoint = pickupPoint
                            };
                            _context.Orders.Add(order);

                            // Добавляем содержимое заказа (если есть)
                            string[] parts = worksheet.Cells[row, 2].Value?.ToString().Split(',');
                            if (parts != null && parts.Length >= 2)
                            {
                                _context.OrderContents.Add(new OrderContents { OrderId = orderId, ProductId = parts[0].Trim(), Quantity = int.Parse(parts[1].Trim()) });
                                if (parts.Length >= 4)
                                {
                                    _context.OrderContents.Add(new OrderContents { OrderId = orderId, ProductId = parts[2].Trim(), Quantity = int.Parse(parts[3].Trim()) });
                                }
                            }
                        }

                        _context.SaveChanges(); // Сохраняем всё (статусы, заказы, содержимое)
                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Orders OFF;");
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private DateTime ParseExcelDate(ExcelRange cell)
        {
            if (cell.Value is double serialDate) return DateTime.FromOADate(serialDate);
            if (cell.Value is DateTime dt) return dt;

            string dateStr = cell.Text?.Trim();
            if (string.IsNullOrEmpty(dateStr)) return DateTime.MinValue;
            if (dateStr == "30.02.2025") dateStr = "28.02.2025";

            if (DateTime.TryParseExact(dateStr, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                return parsedDate;
            }
            return DateTime.MinValue;
        }
    }
}