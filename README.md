# Auth API - JWT Authentication (.NET 8)
## Công việc đã hoàn thành
+ **Cấu hình hệ thống:** Khởi tạo Web API với .NET 8, kết nối SQL Server thông qua Entity Framework Core (Code-First).
+ **Xác thực JWT:** Cấu hình và cấp phát Bearer Token khi đăng nhập thành công để bảo vệ các endpoint.
+ **Thread-Safe Rate Limiting (Chống Spam Đa luồng):** 
    *   Triển khai thuật toán **Cửa sổ trượt (Sliding Window)** để đếm chính xác số lượng request trong thời gian thực.
    *   Sử dụng `ConcurrentDictionary` và `ConcurrentQueue` kết hợp với các thao tác nguyên tử (Atomic Operations) để xử lý đụng độ vùng nhớ.
+ **Aspect-Oriented Programming (AOP):** 
    *   Đóng gói logic Rate Limit thành các `ActionFilterAttribute` độc lập (`[RateLimit]`).
    *   Sử dụng cơ chế Short-circuiting (`OnActionExecuting`) để chặn các request vi phạm ngay từ vòng ngoài
+ **Bảo mật Xác thực (Advanced Security):**
    *   **Mật khẩu:** Băm một chiều bằng **BCrypt** (tự động sinh Salt) chống tấn công Rainbow Table.
    *   **Access Token:** Phân quyền chuẩn RBAC thông qua chữ ký điện tử HMAC-SHA256 (JWT).
    *   **Refresh Token:** Xây dựng cơ chế Token Rotation, tạo chuỗi ngẫu nhiên bằng `RandomNumberGenerator` (CSPRNG - Mật mã học) thay vì hàm Random thông thường.
+ **Kiến trúc Clean Code:**
    *   **Thin Controller:** Controller hoàn toàn không chứa logic nghiệp vụ, ủy quyền 100% qua DI (Dependency Injection).
    *   **Auto Validation:** Bắt lỗi định dạng dữ liệu đầu vào tự động thông qua Data Annotations (`[Required]`, `[RegularExpression]`).

## 📁 Cấu trúc Thư mục Quan trọng
*   `Filters/RateLimit.cs`: Cơ chế chặn Request tiền xử lý (AOP).
*   `Services/RateLimitServices.cs`: Logic tính toán Cửa sổ trượt.
*   `Services/AuthService.cs`: Logic mã hóa BCrypt và sinh Token.
