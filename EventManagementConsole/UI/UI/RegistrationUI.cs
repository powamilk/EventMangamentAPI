using EventManagementConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManagementConsole.UI.Service;

namespace EventManagementConsole.UI.UI
{
    public class RegistrationUI
    {
        private readonly RegistrationService _registrationService;

        public RegistrationUI()
        {
            _registrationService = new RegistrationService();
        }

        public async Task HienThiMenuDangKyAsync()
        {
            while (true)
            {
                Console.WriteLine("===== Quản lý Đăng ký =====");
                Console.WriteLine("1. Liệt kê tất cả đăng ký");
                Console.WriteLine("2. Xem đăng ký theo ID");
                Console.WriteLine("3. Tạo đăng ký mới");
                Console.WriteLine("4. Cập nhật đăng ký");
                Console.WriteLine("5. Xóa đăng ký");
                Console.WriteLine("6. Quay lại Menu chính");
                Console.Write("Chọn tùy chọn: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await LietKeTatCaDangKyAsync();
                        break;
                    case "2":
                        await XemDangKyTheoIdAsync();
                        break;
                    case "3":
                        await TaoDangKyMoiAsync();
                        break;
                    case "4":
                        await CapNhatDangKyAsync();
                        break;
                    case "5":
                        await XoaDangKyAsync();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Tùy chọn không hợp lệ. Vui lòng thử lại.");
                        break;
                }
            }
        }

        private async Task LietKeTatCaDangKyAsync()
        {
            var registrations = await _registrationService.GetAllRegistrationsAsync();
            foreach (var registration in registrations)
            {
                Console.WriteLine($"ID: {registration.Id}, ID Sự kiện: {registration.EventId}, ID Người tham gia: {registration.ParticipantId}");
            }
        }

        private async Task XemDangKyTheoIdAsync()
        {
            Console.Write("Nhập ID đăng ký: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var registration = await _registrationService.GetRegistrationByIdAsync(id);
                if (registration != null)
                {
                    Console.WriteLine($"ID: {registration.Id}, ID Sự kiện: {registration.EventId}, ID Người tham gia: {registration.ParticipantId}");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy đăng ký.");
                }
            }
            else
            {
                Console.WriteLine("ID không hợp lệ.");
            }
        }

        private async Task TaoDangKyMoiAsync()
        {
            Console.Write("Nhập ID sự kiện: ");
            if (!int.TryParse(Console.ReadLine(), out int eventId))
            {
                Console.WriteLine("ID sự kiện không hợp lệ.");
                return;
            }

            Console.Write("Nhập ID người tham gia: ");
            if (!int.TryParse(Console.ReadLine(), out int participantId))
            {
                Console.WriteLine("ID người tham gia không hợp lệ.");
                return;
            }

            var newRegistration = new RegistrationModel
            {
                EventId = eventId,
                ParticipantId = participantId,
                RegistrationDate = DateTime.Now,
                Status = "đã xác nhận"
            };

            await _registrationService.CreateRegistrationAsync(newRegistration);
            Console.WriteLine("Đăng ký đã được tạo thành công.");
        }

        private async Task CapNhatDangKyAsync()
        {
            Console.Write("Nhập ID đăng ký: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var registration = await _registrationService.GetRegistrationByIdAsync(id);
                if (registration == null)
                {
                    Console.WriteLine("Không tìm thấy đăng ký.");
                    return;
                }

                Console.Write("Nhập trạng thái mới (đã xác nhận/đã hủy): ");
                registration.Status = Console.ReadLine();

                await _registrationService.UpdateRegistrationAsync(id, registration);
                Console.WriteLine("Đăng ký đã được cập nhật thành công.");
            }
            else
            {
                Console.WriteLine("ID không hợp lệ.");
            }
        }

        private async Task XoaDangKyAsync()
        {
            Console.Write("Nhập ID đăng ký cần xóa: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                await _registrationService.DeleteRegistrationAsync(id);
                Console.WriteLine("Đăng ký đã được xóa thành công.");
            }
            else
            {
                Console.WriteLine("ID không hợp lệ.");
            }
        }
    }
}

