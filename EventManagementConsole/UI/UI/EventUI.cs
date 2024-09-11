using EventManagementConsole.Model;
using EventManagementConsole.UI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementConsole.UI.UI
{
    public class EventUI
    {
        private readonly EventService _eventService;

        public EventUI()
        {
            _eventService = new EventService();
        }

        public async Task HienThiMenuSuKienAsync()
        {
            while (true)
            {
                Console.WriteLine("===== Quản lý Sự kiện =====");
                Console.WriteLine("1. Liệt kê tất cả sự kiện");
                Console.WriteLine("2. Xem sự kiện theo ID");
                Console.WriteLine("3. Tạo sự kiện mới");
                Console.WriteLine("4. Cập nhật sự kiện");
                Console.WriteLine("5. Xóa sự kiện");
                Console.WriteLine("6. Quay lại Menu chính");
                Console.Write("Chọn tùy chọn: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await LietKeTatCaSuKienAsync();
                        break;
                    case "2":
                        await XemSuKienTheoIdAsync();
                        break;
                    case "3":
                        await TaoSuKienMoiAsync();
                        break;
                    case "4":
                        await CapNhatSuKienAsync();
                        break;
                    case "5":
                        await XoaSuKienAsync();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Tùy chọn không hợp lệ. Vui lòng thử lại.");
                        break;
                }
            }
        }

        private async Task LietKeTatCaSuKienAsync()
        {
            var events = await _eventService.GetAllEventsAsync();
            foreach (var ev in events)
            {
                Console.WriteLine($"ID: {ev.Id}, Tên: {ev.Name}, Địa điểm: {ev.Location}");
            }
        }

        private async Task XemSuKienTheoIdAsync()
        {
            Console.Write("Nhập ID sự kiện: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var ev = await _eventService.GetEventByIdAsync(id);
                if (ev != null)
                {
                    Console.WriteLine($"ID: {ev.Id}, Tên: {ev.Name}, Mô tả: {ev.Description}");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy sự kiện.");
                }
            }
            else
            {
                Console.WriteLine("ID không hợp lệ.");
            }
        }

        private async Task TaoSuKienMoiAsync()
        {
            // Nhập tên sự kiện
            Console.Write("Nhập tên sự kiện: ");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name) || name.Length > 255)
            {
                Console.WriteLine("Tên sự kiện không hợp lệ. Tên không được để trống và phải dưới 255 ký tự.");
                return;
            }

            // Nhập mô tả sự kiện
            Console.Write("Nhập mô tả sự kiện: ");
            var description = Console.ReadLine();
            if (description.Length > 1000)
            {
                Console.WriteLine("Mô tả sự kiện không được vượt quá 1000 ký tự.");
                return;
            }

            // Nhập địa điểm tổ chức
            Console.Write("Nhập địa điểm tổ chức: ");
            var location = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(location) || location.Length > 255)
            {
                Console.WriteLine("Địa điểm không hợp lệ. Địa điểm không được để trống và phải dưới 255 ký tự.");
                return;
            }

            // Nhập thời gian bắt đầu
            Console.Write("Nhập thời gian bắt đầu (định dạng: yyyy-MM-dd HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startTime) || startTime <= DateTime.Now)
            {
                Console.WriteLine("Thời gian bắt đầu không hợp lệ. Thời gian phải là một thời gian hợp lệ và phải lớn hơn hiện tại.");
                return;
            }

            // Nhập thời gian kết thúc
            Console.Write("Nhập thời gian kết thúc (định dạng: yyyy-MM-dd HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime endTime) || endTime <= startTime)
            {
                Console.WriteLine("Thời gian kết thúc không hợp lệ. Thời gian kết thúc phải sau thời gian bắt đầu.");
                return;
            }

            // Nhập số lượng người tham gia tối đa
            Console.Write("Nhập số lượng người tham gia tối đa: ");
            if (!int.TryParse(Console.ReadLine(), out int maxParticipants) || maxParticipants <= 0)
            {
                Console.WriteLine("Số lượng người tham gia tối đa không hợp lệ. Phải là số nguyên dương.");
                return;
            }

            // Tạo đối tượng sự kiện mới
            var newEvent = new EventModel
            {
                Name = name,
                Description = description,
                Location = location,
                StartTime = startTime,
                EndTime = endTime,
                MaxParticipants = maxParticipants
            };

            // Gửi yêu cầu tạo sự kiện
            await _eventService.CreateEventAsync(newEvent);
            Console.WriteLine("Sự kiện đã được tạo thành công.");
        }



        private async Task CapNhatSuKienAsync()
        {
            Console.Write("Nhập ID sự kiện: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var ev = await _eventService.GetEventByIdAsync(id);
                if (ev == null)
                {
                    Console.WriteLine("Không tìm thấy sự kiện.");
                    return;
                }

                Console.Write("Nhập tên sự kiện mới: ");
                ev.Name = Console.ReadLine();
                Console.Write("Nhập mô tả sự kiện mới: ");
                ev.Description = Console.ReadLine();
                Console.Write("Nhập địa điểm tổ chức mới: ");
                ev.Location = Console.ReadLine();

                await _eventService.UpdateEventAsync(id, ev);
                Console.WriteLine("Sự kiện đã được cập nhật thành công.");
            }
            else
            {
                Console.WriteLine("ID không hợp lệ.");
            }
        }

        private async Task XoaSuKienAsync()
        {
            Console.Write("Nhập ID sự kiện cần xóa: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                await _eventService.DeleteEventAsync(id);
                Console.WriteLine("Sự kiện đã được xóa thành công.");
            }
            else
            {
                Console.WriteLine("ID không hợp lệ.");
            }
        }
    }
}
