## THÔNG TIN SINH VIÊN
- **Họ và tên:** Nguyễn Anh Xuân
- **Mã số sinh viên:** 24810320189
- **Lớp:** D19QTANM1
- **Học phần:** Lập trình .NET / Lập trình Windows Forms
- **Tên bài tập:** Bài tập thực hành ngày 30/10 – Bài 5.3, Bài 5.4, Bài 5.5

---

### PHẦN 1. BÀI 5.3 – HỆ THỐNG QUẢN LÝ BÁN HÀNG QUÁN CAFE (CAFE POS SYSTEM)
#### Hình ảnh minh chứng kết quả thực hành

##### 5.3.1 Giao diện chính Quản lý bán hàng quán Cafe & Sơ đồ bàn
Sơ đồ bàn trực quan hiển thị trạng thái bằng màu sắc (xanh: Trống, đỏ: Có khách), khu vực hiển thị hóa đơn và thanh tác vụ gọi món.
![Giao diện chính bài 5.3](./screenshorts/gd5.3.png)

##### 5.3.2 Gọi món và nạp dữ liệu lên DataGridView hóa đơn
Thực hiện chọn món từ thực đơn, nhập số lượng và đồng bộ hiển thị chi tiết hóa đơn của bàn đang chọn.
![Gọi món vào bàn](./screenshorts/kq5.3.png)

##### 5.3.3 Thêm các món khác và tự động cộng dồn tổng tiền
Cập nhật thêm nhiều mặt hàng vào hóa đơn, hệ thống tự động tính thành tiền và cập nhật tổng tiền thanh toán.
![Cộng dồn hóa đơn](./screenshorts/kq5.3%20(2).png)

##### 5.3.4 Nghiệp vụ Chuyển / Đổi bàn ăn
Chuyển toàn bộ danh sách món từ bàn đang ngồi sang bàn trống mục tiêu an toàn bằng Transaction.
![Nghiệp vụ chuyển bàn](./screenshorts/kq5.3%20(3).png)

##### 5.3.5 Thanh toán hóa đơn và hoàn trả trạng thái bàn trống
Thanh toán hóa đơn hiện tại, ghi nhận doanh thu vào CSDL MySQL và đưa bàn về trạng thái sẵn sàng đón khách.
![Thanh toán hóa đơn](./screenshorts/kq5.3%20(4).png)

##### 5.3.6 Kiểm tra xác thực dữ liệu đầu vào (Validation Error)
Hệ thống hiển thị cảnh báo khi xảy ra sai sót nhập liệu hoặc thiếu thông tin khi thao tác thực đơn.
![Báo lỗi xác thực](./screenshorts/loi5.3.png)

---

### PHẦN 2. BÀI 5.4 – HỆ THỐNG QUẢN LÝ ĐẶT PHÒNG KHÁCH SẠN (HOTEL MANAGEMENT SYSTEM)
#### Hình ảnh minh chứng kết quả thực hành

##### 5.4.1 Giao diện chính Quản lý phòng và bộ lọc đa tiêu chí
Khu vực lọc phòng linh hoạt theo loại phòng (Đơn, Đôi, VIP) và trạng thái, kèm danh sách phiếu đang lưu trú.
![Giao diện chính bài 5.4](./screenshorts/gd5.4.png)

##### 5.4.2 Tra cứu CCCD khách hàng và nhận phòng (Check-in)
Tự động nhận diện khách quen qua số CCCD, khóa phòng với cơ chế FOR UPDATE và mở phiếu nhận phòng mới.
![Check-in nhận phòng](./screenshorts/kq5.4.png)

##### 5.4.3 Gọi dịch vụ phát sinh cho khách đang lưu trú
Lựa chọn dịch vụ nước uống, đồ ăn, giặt là kèm số lượng và đồng bộ vào phiếu đặt phòng.
![Gọi dịch vụ phát sinh](./screenshorts/kq5.4%20(2).png)

##### 5.4.4 Danh sách chi tiết các dịch vụ khách đã sử dụng
Hiển thị bảng chi tiết các dịch vụ đã gọi theo mã phiếu đặt phòng trên DataGridView.
![Chi tiết dịch vụ](./screenshorts/kq5.4%20(3).png)

##### 5.4.5 Check-out trả phòng và tính tiền giờ tự động
Hệ thống tự động tính tiền phòng theo số phút lưu trú thực tế bằng TIMESTAMPDIFF, cộng dồn tiền dịch vụ và thanh toán tổng hợp.
![Thanh toán trả phòng](./screenshorts/kq5.4%20(4).png)

##### 5.4.6 Kiểm tra xác thực dữ liệu đầu vào (Validation Error)
Cảnh báo hộp thoại khi thiếu CCCD, phòng đã có người ở hoặc thông tin chưa hợp lệ.
![Báo lỗi xác thực](./screenshorts/loi5.4.png)

---

### PHẦN 3. BÀI 5.5 – HỆ THỐNG QUẢN LÝ NHẬP - XUẤT KHO VẬT TƯ (INVENTORY MANAGEMENT SYSTEM)
#### Hình ảnh minh chứng kết quả thực hành

##### 5.5.1 Giao diện chính Quản lý danh mục vật tư
Màn hình quản lý danh mục vật tư, đơn vị tính và số lượng tồn kho với các chức năng Thêm, Sửa, Xóa và Tìm kiếm.
![Giao diện chính bài 5.5](./screenshorts/gd5.5.png)

##### 5.5.2 Quản lý danh mục nhà cung cấp
Hiển thị danh sách các đối tác cung ứng, liên hệ điện thoại và địa chỉ trụ sở.
![Quản lý nhà cung cấp](./screenshorts/kq5.5.png)

##### 5.5.3 Lập phiếu nhập kho vật tư đa mặt hàng
Màn hình lập phiếu nhập kho, thêm nhiều dòng mặt hàng vào lưới chi tiết tạm thời và tự động tính tổng tiền phiếu.
![Lập phiếu nhập kho](./screenshorts/kq5.5%20(2).png)

##### 5.5.4 Lập phiếu xuất kho vật tư an toàn
Thực hiện xuất kho có kiểm soát lượng tồn, tự động trừ số lượng tồn trong CSDL MySQL bằng Transaction.
![Lập phiếu xuất kho](./screenshorts/kq5.5%20(3).png)

##### 5.5.5 Cảnh báo vật tư có lượng tồn thấp dưới ngưỡng an toàn
Bộ lọc cảnh báo tự động các mặt hàng có số lượng tồn chạm ngưỡng tối thiểu cho phép tùy chỉnh.
![Cảnh báo tồn kho thấp](./screenshorts/kq5.5%20(4).png)

##### 5.5.6 Thống kê tổng giá trị tồn kho toàn hệ thống
Tính toán chính xác tổng giá trị tài sản kho hàng dựa theo phương pháp đơn giá nhập bình quân gia quyền.
![Thống kê tổng giá trị](./screenshorts/kq5.5%20(5).png)

##### 5.5.7 Cảnh báo kiểm soát xuất kho vượt quá số lượng tồn
Hệ thống kích hoạt cơ chế Rollback và thông báo lỗi 'Không đủ hàng trong kho' khi xuất vượt tồn.
![Cảnh báo lỗi xuất kho](./screenshorts/loi5.5.png)
