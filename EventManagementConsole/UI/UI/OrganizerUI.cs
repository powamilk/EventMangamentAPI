using EventManagementConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManagementConsole.UI.Service;

namespace EventManagementConsole.UI.UI
{
    public class OrganizerUI
    {
        private readonly OrganizerService _organizerService;

        public OrganizerUI()
        {
            _organizerService = new OrganizerService();
        }

        public async Task HienThiMenuNguoiToChucAsync()
        {
            while (true)
            {
                Console.WriteLine("===== Quản lý Người tổ chức =====");
                Console.WriteLine("1. Liệt kê tất cả người tổ chức");
                Console.WriteLine("2. Xem người tổ chức theo ID");
                Console.WriteLine("3. Tạo người tổ chức mới");
                Console.WriteLine("4. Cập nhật người tổ chức");
                Console.WriteLine("5. Xóa người tổ chức");
                Console.WriteLine("6. Quay lại Menu chính");
                Console.Write("Chọn tùy chọn: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await LietKeTatCaNguoiToChucAsync();
                        break;
                    case "2":
                        await XemNguoiToChucTheoIdAsync();
                        break;
                    case "3":
                        await TaoNguoiToChucMoiAsync();
                        break;
                    case "4":
                        await CapNhatNguoiToChucAsync();
                        break;
                    case "5":
                        await XoaNguoiToChucAsync();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Tùy chọn không hợp lệ. Vui lòng thử lại.");
                        break;
                }
            }
        }

        private async Task LietKeTatCaNguoiToChucAsync()
        {
            var organizers = await _organizerService.GetAllOrganizersAsync();
            foreach (var organizer in organizers)
            {
                Console.WriteLine($"ID: {organizer.Id}, Tên: {organizer.Name}, Email: {organizer.ContactEmail}");
            }
        }

        private async Task XemNguoiToChucTheoIdAsync()
        {
            Console.Write("Nhập ID người tổ chức: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var organizer = await _organizerService.GetOrganizerByIdAsync(id);
                if (organizer != null)
                {
                    Console.WriteLine($"ID: {organizer.Id}, Tên: {organizer.Name}, Email: {organizer.ContactEmail}");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy người tổ chức.");
                }
            }
            else
            {
                Console.WriteLine("ID không hợp lệ.");
            }
        }

        private async Task TaoNguoiToChucMoiAsync()
        {
            Console.Write("Nhập tên người tổ chức: ");
            var name = Console.ReadLine();
            Console.Write("Nhập email người tổ chức: ");
            var email = Console.ReadLine();
            Console.Write("Nhập số điện thoại người tổ chức: ");
            var phone = Console.ReadLine();

            var newOrganizer = new OrganizerModel
            {
                Name = name,
                ContactEmail = email,
                Phone = phone
            };

            await _organizerService.CreateOrganizerAsync(newOrganizer);
            Console.WriteLine("Người tổ chức đã được tạo thành công.");
        }

        private async Task CapNhatNguoiToChucAsync()
        {
            Console.Write("Nhập ID người tổ chức: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var organizer = await _organizerService.GetOrganizerByIdAsync(id);
                if (organizer == null)
                {
                    Console.WriteLine("Không tìm thấy người tổ chức.");
                    return;
                }

                Console.Write("Nhập tên mới: ");
                organizer.Name = Console.ReadLine();
                Console.Write("Nhập email mới: ");
                organizer.ContactEmail = Console.ReadLine();
                Console.Write("Nhập số điện thoại mới: ");
                organizer.Phone = Console.ReadLine();

                await _organizerService.UpdateOrganizerAsync(id, organizer);
                Console.WriteLine("Người tổ chức đã được cập nhật thành công.");
            }
            else
            {
                Console.WriteLine("ID không hợp lệ.");
            }
        }

        private async Task XoaNguoiToChucAsync()
        {
            Console.Write("Nhập ID người tổ chức cần xóa: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                await _organizerService.DeleteOrganizerAsync(id);
                Console.WriteLine("Người tổ chức đã được xóa thành công.");
            }
            else
            {
                Console.WriteLine("ID không hợp lệ.");
            }
        }
    }
}
