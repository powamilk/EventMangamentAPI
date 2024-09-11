using EventManagementConsole.UI.UI;
using System.Text;

class Program
{
    static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        while (true)
        {
            Console.WriteLine("===== Menu chính =====");
            Console.WriteLine("1. Quản lý Sự kiện");
            Console.WriteLine("2. Quản lý Người tổ chức");
            Console.WriteLine("3. Quản lý Người tham gia");
            Console.WriteLine("4. Quản lý Đăng ký");
            Console.WriteLine("5. Quản lý Đánh giá");
            Console.WriteLine("6. Thoát");
            Console.Write("Chọn tùy chọn: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var eventUI = new EventUI();
                    await eventUI.HienThiMenuSuKienAsync();
                    break;
                case "2":
                    var organizerUI = new OrganizerUI();
                    await organizerUI.HienThiMenuNguoiToChucAsync();
                    break;
                case "3":
                    var participantUI = new ParticipantUI();
                    await participantUI.HienThiMenuNguoiThamGiaAsync();
                    break;
                case "4":
                    var registrationUI = new RegistrationUI();
                    await registrationUI.HienThiMenuDangKyAsync();
                    break;
                case "5":
                    var reviewUI = new ReviewUI();
                    await reviewUI.HienThiMenuDanhGiaAsync();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Tùy chọn không hợp lệ. Vui lòng thử lại.");
                    break;
            }
        }
    }
}