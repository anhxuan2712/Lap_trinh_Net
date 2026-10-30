# BÁO CÁO BÀI TẬP THỰC HÀNH LẬP TRÌNH C# / WINDOWS FORMS (30/10)

## I. THÔNG TIN SINH VIÊN
- **Họ và tên:** Nguyễn Anh Xuân
- **Mã số sinh viên:** 24810320189
- **Lớp:** D19QTANM1
- **Học phần:** Lập trình .NET / Lập trình Windows Forms
- **Tên bài tập:** Báo cáo bài tập thực hành ngày 30/10 (Bài 5.3, Bài 5.4, Bài 5.5)

---

## II. DANH SÁCH BÀI TẬP HOÀN THÀNH & TỰ ĐÁNH GIÁ

| Bài tập | Tiêu đề bài tập | Thư mục dự án | Kiến trúc / Cơ sở dữ liệu | Trạng thái | Mức độ hoàn thành (%) |
| :---: | :--- | :---: | :---: | :---: | :---: |
| **Bài 5.3** | Hệ thống Quản lý Bán hàng & Gọi món Quán Cafe (Cafe POS) | [`Bai5.3`](./Bai5.3) | Repository Pattern / MySQL (`CafePOSDB`) | Hoàn thành | 100% |
| **Bài 5.4** | Hệ thống Quản lý Đặt phòng & Dịch vụ Khách sạn (Hotel Management System) | [`Bai5.4`](./Bai5.4) | Mô hình 3 lớp (3-Tier: BUS - DAL - DTO) / MySQL (`HotelDB`) | Hoàn thành | 100% |
| **Bài 5.5** | Hệ thống Quản lý Nhập - Xuất Kho & Kiểm kê Vật tư (Inventory Management) | [`Bai5.5`](./Bai5.5) | Mô hình 3 lớp (3-Tier: BUS - DAL - DTO) / MySQL (`InventoryDB`) | Hoàn thành | 100% |

---

## III. MÔI TRƯỜNG & HƯỚNG DẪN CẤU HÌNH CƠ SỞ DỮ LIỆU

### 1. Môi trường phát triển & Thư viện sử dụng
- **Nền tảng:** .NET 10.0 (Windows Forms SDK).
- **Hệ quản trị CSDL:** MySQL Server 8.0+ / MariaDB (Port mặc định: `3306`).
- **Thư viện kết nối CSDL (NuGet Packages):**
  - `MySql.Data` (v26.10.0) / `MySqlConnector` (v2.6.2): Cung cấp bộ thư viện kết nối ADO.NET thuần hiệu năng cao cho MySQL.

### 2. Thiết lập Cơ sở dữ liệu
Trước khi khởi chạy các dự án, tiến hành import các tệp tin script SQL tương ứng vào MySQL server:
1. **Bài 5.3:** Mở và thực thi tệp [`Bai5.3/Bai5.3/CafePOSDB.sql`](./Bai5.3/Bai5.3/CafePOSDB.sql) để tạo database `CafePOSDB` cùng các bảng `BanAn`, `DanhMuc`, `MonAn`, `HoaDon`, `ChiTietHoaDon` và dữ liệu mẫu ban đầu.
2. **Bài 5.4:** Mở và thực thi tệp [`Bai5.4/HotelDB.sql`](./Bai5.4/HotelDB.sql) để tạo database `HotelDB` cùng các bảng `Phong`, `KhachHang`, `DichVu`, `PhieuDatPhong`, `SuDungDichVu`.
3. **Bài 5.5:** Mở và thực thi tệp [`Bai5.5/Database/InventoryDB.sql`](./Bai5.5/Database/InventoryDB.sql) để tạo database `InventoryDB` cùng các bảng `NhaCungCap`, `VatTu`, `PhieuKho`, `ChiTietPhieuKho`.

### 3. Cấu hình chuỗi kết nối (Connection String)
Chuỗi kết nối chuẩn được định nghĩa tại các lớp DAL / Repository:
```csharp
Server=localhost;Port=3306;Database=<Ten_CSDL>;Uid=root;Pwd=familo23;CharSet=utf8mb4;
```
*(Lưu ý: Có thể tùy chỉnh tham số `Uid` và `Pwd` phù hợp với tài khoản quản trị MySQL cục bộ).*

---

## IV. CHI TIẾT KẾT QUẢ THỰC HÀNH CÁC BÀI TẬP

### 1. BÀI 5.3 – HỆ THỐNG QUẢN LÝ BÁN HÀNG & GỌI MÓN QUÁN CAFE (CAFE POS)

#### 1.1. Mục tiêu & Mô tả bài toán
Xây dựng phân hệ Point-of-Sale (POS) cho quán cà phê nhằm tối ưu hóa quy trình gọi món, đổi bàn và thanh toán:
- Quản lý sơ đồ bàn trực quan bằng mã nguồn sinh động qua `FlowLayoutPanel`, phân biệt trạng thái bàn trống và bàn đang có khách bằng màu sắc trực quan (`LightGreen` vs `LightCoral`).
- Xử lý gọi món, tùy chỉnh số lượng, tự động sinh hóa đơn khi bàn bắt đầu có khách và cộng dồn số lượng khi gọi thêm món.
- Hỗ trợ nghiệp vụ chuyển/đổi bàn ăn linh hoạt, chuyển toàn bộ hóa đơn từ bàn cũ sang bàn mới an toàn.
- Thanh toán hóa đơn: tự động chốt giờ ra (`GioRa = NOW()`), cập nhật trạng thái hóa đơn sang đã thanh toán và hoàn trả bàn về trạng thái "Trống".
- Phân hệ quản lý thực đơn (Menu Management): Thêm mới, chỉnh sửa thông tin món ăn, danh mục, cập nhật đơn giá và xóa món khỏi danh mục.

#### 1.2. Thiết kế Cơ sở dữ liệu & Kiến trúc mã nguồn
- **Cơ sở dữ liệu:** `CafePOSDB` với các ràng buộc khóa ngoại `FOREIGN KEY` và ràng buộc duy nhất `UQ_ChiTiet_HoaDon_Mon (MaHoaDon, MaMon)`.
- **Kiến trúc:** Áp dụng **Repository Pattern** qua lớp [`CafeRepository.cs`](./Bai5.3/Bai5.3/CafeRepository.cs):
  - Tách bạch hoàn toàn tầng tương tác dữ liệu với giao diện.
  - Sử dụng `MySqlTransaction` trong thao tác đổi bàn và thanh toán nhằm đảm bảo tính toàn vẹn dữ liệu (Atomicity).

#### 1.3. Hình ảnh minh chứng kết quả thực hành
##### 5.3.1 Giao diện chính màn hình Bán hàng (POS) & Sơ đồ bàn trực quan
Hiển thị danh sách các bàn với màu sắc phân biệt trạng thái bàn ăn, khu vực hiển thị hóa đơn và thanh tác vụ gọi món.
![Giao diện POS bán hàng](./docs/bai5.3_pos_main.png)
*Mô tả: Sơ đồ bàn trực quan, bàn xanh biểu thị bàn Trống, bàn đỏ biểu thị bàn đang Có khách.*

##### 5.3.2 Gọi món & Đồng bộ hóa đơn lên DataGridView
Thực hiện chọn món từ danh sách, nhập số lượng và nạp vào hóa đơn của bàn đang chọn.
![Gọi món vào bàn](./docs/bai5.3_order.png)
*Mô tả: Danh sách món ăn đã gọi hiển thị chi tiết số lượng, đơn giá và thành tiền.*

##### 5.3.3 Nghiệp vụ Chuyển / Đổi bàn ăn
Chuyển toàn bộ danh sách món từ bàn đang ngồi sang bàn trống mục tiêu.
![Nghiệp vụ đổi bàn](./docs/bai5.3_move_table.png)
*Mô tả: Chọn bàn đích và nhấn 'Đổi bàn', hệ thống tự động cập nhật lại trạng thái của cả hai bàn.*

##### 5.3.4 Thanh toán hóa đơn & Trả bàn
Thanh toán hóa đơn hiện tại, ghi nhận doanh thu và đưa bàn về trạng thái sẵn sàng phục vụ.
![Thanh toán hóa đơn](./docs/bai5.3_checkout.png)
*Mô tả: Hộp thoại xác nhận thanh toán thành công và làm mới sơ đồ bàn.*

##### 5.3.5 Phân hệ Quản lý danh mục Thực đơn
Giao diện quản lý danh sách món ăn, cho phép thêm món mới, cập nhật giá và phân loại danh mục.
![Quản lý thực đơn](./docs/bai5.3_menu_manager.png)
*Mô tả: Thêm, sửa, xóa món ăn với cơ chế kiểm tra tính hợp lệ của dữ liệu đầu vào.*

---

### 2. BÀI 5.4 – HỆ THỐNG QUẢN LÝ ĐẶT PHÒNG & DỊCH VỤ KHÁCH SẠN (HOTEL MANAGEMENT SYSTEM)

#### 2.1. Mục tiêu & Mô tả bài toán
Xây dựng phần mềm quản lý lưu trú và dịch vụ phát sinh trong khách sạn áp dụng mô hình 3 lớp tiêu chuẩn:
- **Quản lý phòng & Check-in:** Bộ lọc danh sách phòng thông minh theo nhiều tiêu chí (Loại phòng: Đơn, Đôi, VIP; Trạng thái: Trống, Đang ở). Tra cứu nhanh thông tin khách quen qua CCCD (tự động nạp Họ tên, SĐT) hoặc thêm mới khách hàng.
- **Cơ chế Check-in an toàn:** Sử dụng Transaction kết hợp mệnh đề khóa bi quan `FOR UPDATE` trong MySQL để chống tình trạng đặt phòng trùng lặp (Race Condition).
- **Phân hệ Gọi dịch vụ:** Tra cứu phiếu đặt phòng theo CCCD khách đang lưu trú, chọn dịch vụ (Nước uống, Giặt là, Đồ ăn...), chọn số lượng và ghi nhận vào phiếu sử dụng.
- **Check-out & Tính toán hóa đơn tự động:** Tự động tính thời gian lưu trú thực tế theo giờ/phút bằng hàm `TIMESTAMPDIFF(MINUTE, p.NgayNhanPhong, NOW())`, nhân với đơn giá giờ của hạng phòng tương ứng, cộng dồn toàn bộ tiền dịch vụ đã gọi và hoàn tất thanh toán.

#### 2.2. Thiết kế Mô hình 3 lớp (3-Tier Architecture)
- **DTO (Data Transfer Object):** `KhachHangDTO`, `PhongDTO`, `DichVuDTO`, `PhieuDatPhongDTO`, `SuDungDichVuDTO` đại diện cho các thực thể dữ liệu trong hệ thống.
- **DAL (Data Access Layer):** Lớp [`HotelDAL.cs`](./Bai5.4/Bai5.4/DAL/HotelDAL.cs) chịu trách nhiệm kết nối, thực thi truy vấn tham số hóa (Parameterized Queries) và điều phối giao dịch Transaction an toàn.
- **BUS (Business Logic Layer):** Lớp [`HotelBUS.cs`](./Bai5.4/Bai5.4/BUS/HotelBUS.cs) xử lý toàn bộ logic nghiệp vụ trước khi chuyển dữ liệu cho tầng DAL hoặc trả về tầng UI.

#### 2.3. Hình ảnh minh chứng kết quả thực hành
##### 5.4.1 Màn hình Quản lý phòng & Bộ lọc đa tiêu chí
Giao diện phân loại phòng, hiển thị thông tin giá giờ và danh sách khách đang lưu trú.
![Màn hình quản lý phòng](./docs/bai5.4_room_filter.png)
*Mô tả: Lọc phòng linh hoạt theo loại phòng và trạng thái; danh sách phiếu đang ở hiển thị số phút lưu trú thực tế.*

##### 5.4.2 Nghiệp vụ Tra cứu CCCD & Check-in nhận phòng
Tra cứu thông tin khách hàng bằng số CCCD và tiến hành tạo phiếu nhận phòng.
![Check-in nhận phòng](./docs/bai5.4_checkin.png)
*Mô tả: Tự động điền thông tin khách quen và mở phiếu nhận phòng mới.*

##### 5.4.3 Phân hệ Gọi dịch vụ phát sinh
Lựa chọn dịch vụ kèm số lượng cho khách hàng đang lưu trú trong khách sạn.
![Gọi dịch vụ](./docs/bai5.4_service.png)
*Mô tả: Đồng bộ danh sách dịch vụ đã sử dụng theo từng mã phiếu đặt phòng.*

##### 5.4.4 Check-out trả phòng & Thanh toán tổng hợp
Tổng hợp chi phí phòng theo thời gian thực kết hợp phí dịch vụ để ra tổng số tiền thanh toán.
![Thanh toán check-out](./docs/bai5.4_checkout.png)
*Mô tả: Hộp thoại thông báo chi tiết tổng tiền thanh toán và chuyển trạng thái phòng về 'Trống'.*

---

### 3. BÀI 5.5 – HỆ THỐNG QUẢN LÝ NHẬP - XUẤT KHO & KIỂM KÊ VẬT TƯ (INVENTORY MANAGEMENT)

#### 3.1. Mục tiêu & Mô tả bài toán
Phát triển hệ thống quản lý kho hàng toàn diện, đảm bảo kiểm soát chặt chẽ luồng hàng nhập - xuất và số lượng tồn kho theo thời gian thực:
- **Quản lý danh mục Vật tư & Nhà cung cấp:** Đầy đủ các thao tác Thêm, Sửa, Xóa và Tìm kiếm tức thời theo từ khóa (Mã vật tư, Tên vật tư, Tên NCC, Số điện thoại).
- **Lập phiếu Kho (Nhập kho / Xuất kho):** Hỗ trợ lập phiếu nhiều dòng mặt hàng (`ChiTietPhieuKho`) cùng lúc qua `BindingList<ChiTietPhieuKhoDTO>` và `DataGridView`.
- **Ràng buộc nghiệp vụ kiểm soát tồn kho toàn vẹn:**
  - Phiếu Nhập (`NHAP`): Bắt buộc chọn Nhà cung cấp, tự động cộng dồn số lượng nhập vào `SoLuongTon` của vật tư.
  - Phiếu Xuất (`XUAT`): Tự động kiểm tra số lượng tồn hiện có; nếu xuất vượt quá tồn kho, hệ thống áp dụng câu lệnh cập nhật có điều kiện `SoLuongTon >= @SoLuong` kết hợp Transaction để hủy bỏ toàn bộ thao tác (`Rollback`) và thông báo lỗi.
- **Báo cáo & Cảnh báo an toàn kho:**
  - Lọc và cảnh báo danh sách vật tư có lượng tồn thấp dưới ngưỡng tối thiểu (`Threshold`) có thể tùy chỉnh linh hoạt qua `NumericUpDown`.
  - Thống kê tổng giá trị tồn kho toàn hệ thống dựa trên công thức tính giá nhập bình quân gia quyền.

#### 3.2. Thiết kế Mô hình 3 lớp & Ràng buộc toàn vẹn CSDL
- **Tầng DTO:** [`InventoryModels.cs`](./Bai5.5/Bai5.5/DTO/InventoryModels.cs) định nghĩa chuẩn hóa các mô hình `VatTuDTO`, `NhaCungCapDTO`, `PhieuKhoDTO`, `ChiTietPhieuKhoDTO`.
- **Tầng DAL:** [`InventoryDAL.cs`](./Bai5.5/Bai5.5/DAL/InventoryDAL.cs) điều phối giao dịch cấp phát mã phiếu tự động, ghi chi tiết phiếu và cập nhật tồn kho nguyên tử trong cùng một Transaction.
- **Tầng BUS:** [`InventoryBUS.cs`](./Bai5.5/Bai5.5/BUS/InventoryBUS.cs) kiểm tra hợp lệ dữ liệu: không cho phép một vật tư xuất hiện hai lần trong một phiếu, kiểm tra đơn giá không âm, số lượng phải lớn hơn 0.

#### 3.3. Hình ảnh minh chứng kết quả thực hành
##### 5.5.1 Màn hình Quản lý danh mục Vật tư
Giao diện quản lý danh mục vật tư, đơn vị tính và số lượng tồn kho.
![Quản lý danh mục vật tư](./docs/bai5.5_vattu.png)
*Mô tả: Tìm kiếm nhanh theo mã/tên và thực hiện các thao tác Thêm, Sửa, Xóa vật tư.*

##### 5.5.2 Màn hình Quản lý danh mục Nhà cung cấp
Quản lý thông tin đối tác cung ứng, liên hệ điện thoại và địa chỉ trụ sở.
![Quản lý nhà cung cấp](./docs/bai5.5_nhacungcap.png)
*Mô tả: Quản lý danh mục nhà cung cấp với kiểm tra ràng buộc số điện thoại duy nhất.*

##### 5.5.3 Phân hệ Lập phiếu Nhập / Xuất kho đa mặt hàng
Màn hình tạo phiếu kho, thêm nhiều chi tiết mặt hàng và tự động tính tổng tiền phiếu.
![Lập phiếu kho](./docs/bai5.5_phieukho.png)
*Mô tả: Thêm các mặt hàng vào lưới chi tiết tạm thời trước khi lưu giao dịch vào CSDL.*

##### 5.5.4 Cảnh báo kiểm soát xuất kho vượt quá số lượng tồn
Hệ thống kích hoạt cơ chế Rollback và cảnh báo khi số lượng xuất lớn hơn tồn kho.
![Cảnh báo lỗi xuất kho](./docs/bai5.5_error_stock.png)
*Mô tả: Thông báo lỗi nghiệp vụ 'Không đủ hàng trong kho' nhằm ngăn ngừa sai lệch số liệu tồn kho.*

##### 5.5.5 Phân hệ Báo cáo tồn kho & Cảnh báo tồn thấp dưới ngưỡng
Thống kê tổng giá trị kho hàng và lọc danh sách các vật tư chạm ngưỡng cảnh báo.
![Báo cáo tồn kho](./docs/bai5.5_baocao.png)
*Mô tả: Cảnh báo vật tư dưới ngưỡng an toàn và tổng hợp giá trị vốn tồn kho.*

---

## V. TỔNG KẾT KỸ THUẬT TIÊU BIỂU NÂNG CAO ĐÃ ÁP DỤNG

1. **Kiến trúc phân lớp chuẩn mực (3-Tier & Repository Pattern):** Phân chia rõ ràng giữa tầng hiển thị (GUI), tầng nghiệp vụ (BUS) và tầng truy cập dữ liệu (DAL), tạo tiền đề cho việc mở rộng, bảo trì và kiểm thử độc lập.
2. **Giao dịch Cơ sở dữ liệu (Database Transactions & Concurrency Control):** Áp dụng `MySqlTransaction` và kỹ thuật khóa dòng bi quan `FOR UPDATE` trong các nghiệp vụ quan trọng (đặt phòng, đổi bàn, xuất nhập kho), đảm bảo nguyên tắc ACID (Atomicity, Consistency, Isolation, Durability).
3. **Data Binding & Dynamic UI Components:** Kết hợp cơ chế liên kết dữ liệu 2 chiều (`BindingList<T>`, `BindingSource`) với việc vẽ và cấu hình các điều khiển giao diện động (`FlowLayoutPanel`, `TabControl`, `DataGridView`) hoàn toàn bằng mã nguồn C# linh hoạt, đáp ứng thẩm mỹ hiện đại.
4. **An toàn dữ liệu với Parameterized Queries:** 100% các câu lệnh truy vấn dữ liệu SQL đều được tham số hóa qua `MySqlParameter`, triệt tiêu hoàn toàn lỗ hổng bảo mật tấn công SQL Injection.

