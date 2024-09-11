using EventManagementConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManagementConsole.UI.Service;

namespace EventManagementConsole.UI.UI
{
    public class ParticipantUI
    {
        private readonly ParticipantService _participantService;

        public ParticipantUI()
        {
            _participantService = new ParticipantService();
        }

        public async Task HienThiMenuNguoiThamGiaAsync()
        {
            while (true)
            {
                Console.WriteLine("===== Quản lý Người tham gia =====");
                Console.WriteLine("1. Liệt kê tất cả người tham gia");
                Console.WriteLine("2. Xem người tham gia theo ID");
                Console.WriteLine("3. Tạo người tham gia mới");
                Console.WriteLine("4. Cập nhật người tham gia");
                Console.WriteLine("5. Xóa người tham gia");
                Console.WriteLine("6. Quay lại Menu chính");
                Console.Write("Chọn tùy chọn: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await LietKeTatCaNguoiThamGiaAsync();
                        break;
                    case "2":
                        await XemNguoiThamGiaTheoIdAsync();
                        break;
                    case "3":
                        await TaoNguoiThamGiaMoiAsync();
                        break;
                    case "4":
                        await CapNhatNguoiThamGiaAsync();
                        break;
                    case "5":
                        await XoaNguoiThamGiaAsync();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Tùy chọn không hợp lệ. Vui lòng thử lại.");
                        break;
                }
            }
        }

        private async Task LietKeTatCaNguoiThamGiaAsync()
        {
            var participants = await _participantService.GetAllParticipantsAsync();
            foreach (var participant in participants)
            {
                Console.WriteLine($"ID: {participant.Id}, Tên: {participant.Name}, Email: {participant.Email}");
            }
        }

        private async Task XemNguoiThamGiaTheoIdAsync()
        {
            Console.Write("Nhập ID người tham gia: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var participant = await _participantService.GetParticipantByIdAsync(id);
                if (participant != null)
                {
                    Console.WriteLine($"ID: {participant.Id}, Tên: {participant.Name}, Email: {participant.Email}");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy người tham gia.");
                }
            }
            else
            {
                Console.WriteLine("ID không hợp lệ.");
            }
        }

        private async Task TaoNguoiThamGiaMoiAsync()
        {
            Console.Write("Nhập tên người tham gia: ");
            var name = Console.ReadLine();
            Console.Write("Nhập email người tham gia: ");
            var email = Console.ReadLine();
            Console.Write("Nhập số điện thoại người tham gia: ");
            var phone = Console.ReadLine();

            var newParticipant = new ParticipantModel
            {
                Name = name,
                Email = email,
                Phone = phone,
                RegisteredAt = DateTime.Now
            };

            await _participantService.CreateParticipantAsync(newParticipant);
            Console.WriteLine("Người tham gia đã được tạo thành công.");
        }

        private async Task CapNhatNguoiThamGiaAsync()
        {
            Console.Write("Nhập ID người tham gia: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var participant = await _participantService.GetParticipantByIdAsync(id);
                if (participant == null)
                {
                    Console.WriteLine("Không tìm thấy người tham gia.");
                    return;
                }

                Console.Write("Nhập tên mới: ");
                participant.Name = Console.ReadLine();
                Console.Write("Nhập email mới: ");
                participant.Email = Console.ReadLine();
                Console.Write("Nhập số điện thoại mới: ");
                participant.Phone = Console.ReadLine();

                await _participantService.UpdateParticipantAsync(id, participant);
                Console.WriteLine("Người tham gia đã được cập nhật thành công.");
            }
            else
            {
                Console.WriteLine("ID không hợp lệ.");
            }
        }

        private async Task XoaNguoiThamGiaAsync()
        {
            Console.Write("Nhập ID người tham gia cần xóa: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                await _participantService.DeleteParticipantAsync(id);
                Console.WriteLine("Người tham gia đã được xóa thành công.");
            }
            else
            {
                Console.WriteLine("ID không hợp lệ.");
            }
        }
    }
}
