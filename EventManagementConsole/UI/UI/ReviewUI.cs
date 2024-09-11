using EventManagementConsole.Model;
using EventManagementConsole.UI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EventManagementConsole.UI.UI
{
    public class ReviewUI
    {
        private readonly ReviewService _reviewService;

        public ReviewUI()
        {
            _reviewService = new ReviewService();
        }

        public async Task HienThiMenuDanhGiaAsync()
        {
            while (true)
            {
                Console.WriteLine("===== Quản lý Đánh giá =====");
                Console.WriteLine("1. Liệt kê tất cả đánh giá");
                Console.WriteLine("2. Xem đánh giá theo ID");
                Console.WriteLine("3. Tạo đánh giá mới");
                Console.WriteLine("4. Cập nhật đánh giá");
                Console.WriteLine("5. Xóa đánh giá");
                Console.WriteLine("6. Quay lại Menu chính");
                Console.Write("Chọn tùy chọn: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await LietKeTatCaDanhGiaAsync();
                        break;
                    case "2":
                        await XemDanhGiaTheoIdAsync();
                        break;
                    case "3":
                        await TaoDanhGiaMoiAsync();
                        break;
                    case "4":
                        await CapNhatDanhGiaAsync();
                        break;
                    case "5":
                        await XoaDanhGiaAsync();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Tùy chọn không hợp lệ. Vui lòng thử lại.");
                        break;
                }
            }
        }

        private async Task LietKeTatCaDanhGiaAsync()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            foreach (var review in reviews)
            {
                Console.WriteLine($"ID: {review.Id}, ID Sự kiện: {review.EventId}, Điểm: {review.Rating}");
            }
        }

        private async Task XemDanhGiaTheoIdAsync()
        {
            Console.Write("Nhập ID đánh giá: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var review = await _reviewService.GetReviewByIdAsync(id);
                if (review != null)
                {
                    Console.WriteLine($"ID: {review.Id}, ID Sự kiện: {review.EventId}, Điểm: {review.Rating}, Nhận xét: {review.Comment}");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy đánh giá.");
                }
            }
            else
            {
                Console.WriteLine("ID không hợp lệ.");
            }
        }

        private async Task TaoDanhGiaMoiAsync()
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

            Console.Write("Nhập điểm (1-5): ");
            if (!float.TryParse(Console.ReadLine(), out float rating) || rating < 1 || rating > 5)
            {
                Console.WriteLine("Điểm không hợp lệ.");
                return;
            }

            Console.Write("Nhập nhận xét: ");
            var comment = Console.ReadLine();

            var newReview = new ReviewModel
            {
                EventId = eventId,
                ParticipantId = participantId,
                Rating = rating,
                Comment = comment,
                CreatedAt = DateTime.Now
            };

            await _reviewService.CreateReviewAsync(newReview);
            Console.WriteLine("Đánh giá đã được tạo thành công.");
        }

        private async Task CapNhatDanhGiaAsync()
        {
            Console.Write("Nhập ID đánh giá: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var review = await _reviewService.GetReviewByIdAsync(id);
                if (review == null)
                {
                    Console.WriteLine("Không tìm thấy đánh giá.");
                    return;
                }

                Console.Write("Nhập điểm mới (1-5): ");
                if (!float.TryParse(Console.ReadLine(), out float rating) || rating < 1 || rating > 5)
                {
                    Console.WriteLine("Điểm không hợp lệ.");
                    return;
                }
                review.Rating = rating;

                Console.Write("Nhập nhận xét mới: ");
                review.Comment = Console.ReadLine();

                await _reviewService.UpdateReviewAsync(id, review);
                Console.WriteLine("Đánh giá đã được cập nhật thành công.");
            }
            else
            {
                Console.WriteLine("ID không hợp lệ.");
            }
        }

        private async Task XoaDanhGiaAsync()
        {
            Console.Write("Nhập ID đánh giá cần xóa: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                await _reviewService.DeleteReviewAsync(id);
                Console.WriteLine("Đánh giá đã được xóa thành công.");
            }
            else
            {
                Console.WriteLine("ID không hợp lệ.");
            }
        }
    }
}
