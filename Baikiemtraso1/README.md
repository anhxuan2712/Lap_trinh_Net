Phần I — Lý thuyết

1) Kiểu giá trị vs Kiểu tham chiếu (cơ chế lưu trữ)
- Kiểu giá trị (struct, các kiểu nguyên thủy như int, bool, char) lưu trực tiếp giá trị. Chúng thường được cấp phát trên stack hoặc được nhúng (inlined) bên trong các đối tượng/mảng. Việc sao chép một kiểu giá trị sẽ sao chép dữ liệu; mỗi bản sao hoạt động độc lập.
- Kiểu tham chiếu (class, array, delegate) lưu một tham chiếu tới đối tượng trên heap. Việc sao chép một tham chiếu chỉ sao chép con trỏ (reference); nhiều tham chiếu có thể trỏ tới cùng một đối tượng trên heap. Vòng đời của đối tượng trên heap được quản lý bởi trình thu gom rác (garbage collector).

2) Thuộc tính init-only (C# 9/10) so với set thông thường
- Thuộc tính init cho phép gán chỉ trong quá trình khởi tạo đối tượng (trong constructor hoặc object initializer). Sau khi khởi tạo hoàn tất, các thuộc tính này trở nên bất biến (immutable).
- Trái lại, accessor set thông thường cho phép thay đổi giá trị bất cứ lúc nào.
- Trường hợp sử dụng: các DTO/record hoặc đối tượng cấu hình bất biến, muốn sử dụng object initializer để khởi tạo nhưng ngăn chặn thay đổi sau đó.

3) virtual vs override (đa hình)
- virtual (khai báo ở lớp cơ sở): đánh dấu phương thức có thể được ghi đè và cung cấp một cài đặt mặc định.
- override (khai báo ở lớp dẫn xuất): ghi đè cài đặt của lớp cơ sở. Tại thời điểm chạy, lời gọi đến phương thức được phân phát động (dynamic dispatch) tới phiên bản ghi đè ở lớp con, cho phép hành vi đa hình.

4) Tại sao thành phần static không thể truy xuất qua một thể hiện
- Các thành phần static thuộc về kiểu (type) chứ không thuộc về bất kỳ thể hiện (instance) nào. Chúng biểu diễn trạng thái hoặc hành vi chia sẻ ở cấp lớp. Nếu cho phép truy cập qua instance sẽ gây nhầm lẫn vì không tồn tại bản sao riêng cho từng instance. Ngôn ngữ yêu cầu truy cập thành phần static thông qua tên kiểu để ý định rõ ràng.
