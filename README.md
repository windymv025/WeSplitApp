# Tên pject: WeSplitApp
WPF .NET framework

# Thông tin nhóm:
| MSSV     |     Họ và tên    | Email                         |
|:--------:|:----------------:|:-----------------------------:|
| 18120655 | Phạm Minh Vương  | 18120655@student.hcmus.edu.vn |
| 18120568 | Phạm Văn Thật    | 18120568@student.hcmus.edu.vn |
| 18120653 | Lưu Trường Vũ    | 18120653@student.hcmus.edu.vn |
|:--------:|:----------------:|:-----------------------------:|

# Chức năng đã hoàn thành:
1. Splash Screen (0.5 điểm):
	- Hiển thị thông tin chào mừng mỗi khi ứng dụng khởi chạy.
	- Mỗi lần hiện ngẫu nhiên một thông tin thú vị về một địa điểm du lịch.
	- Cho phép chọn check "Không hiện hộp thoại này mỗi khi khởi động". Từ nay về sau đi thẳng vào màn hình HomeScreen luôn.

2. HomeScreen (4 điểm):
	- Liệt kê danh sách các chuyến đi, phân ra theo 2 loại: đã từng đi trước đó và đang đi.
	- Xem chi tiết các chuyến đi: Danh sách các thành viên, Danh sách các địa điểm, tổng kết các mục thu chi của cả nhóm (vẽ biểu đồ hình bánh).

3. SearchScreen (2 điểm):
	- Tìm kiếm chuyến đi theo tên địa điểm, tên thành viên trong chuyến đi.

4. CreateJourneyScreen  (1 điểm):
	- Cho phép trưởng nhóm tạo mới một chuyến đi với các thông tin:
		+ Tên chuyến đi
		+ Thêm các thành viên
		+ Thêm các khoản chi

5. UpdateJourneyScreen (2.5 điểm):
	- Cập nhật thông tin của chuyến đi: các thành viên, các hình ảnh, các mốc lộ trình, các khoản thu chi. Chú ý nếu có người ứng trước cần báo cáo ai phải trả cho ai bao nhiêu tiền.

# Các chức năng đã thêm:
	- Dùng cơ sở dữ liệu MS SQL Server để lưu trữ dữ liệu. (File query "DBWeSplit.sql" nằm trong thư mục ".\Source code\Database")
	- Dùng full-text search để tìm kiếm chuyến đi theo tên thành viên hoặc tên chuyến đi.
	- Xử lí thay đổi kích thước cửa sổ ứng dụng.
	- Giao diện tự co giãn khi thay đổi kích thước.
	- Thêm chức năng placeholder cho textbox.
	- Giao diện đẹp, dễ nhìn, dễ sử dụng.
	- Menu hamburger.
	- Phân trang.

# Điểm đề nghị: 10 điểm

# Thư viện và package đã dùng:
	- MaterialDesign. (Hướng dẫn cài đặt: http://materialdesigninxaml.net/)
	- EntityFramework.
	- LiveChart.wpf

# Xử lý con đường bất hạnh:
	- Tránh bị hư phần mềm khi phóng to thu nhỏ.
	- Xử lý phân trang hiệu quả khi search và khi đổi chế độ search (tìm kiếm theo tên chuyên đi hoặc theo tên thành viên).
	- Xứ lý lỗi khi thêm chuyến đi mà người dùng nhập thiếu thông tin.
	- Xử lý lưu ảnh vào thư mục Images với tên không bị trùng lắp.

# Link youtube demo: 
	https://youtu.be/-1H9VooXQsw

# Ghi chú:
	*Để chạy được project cần thực hiện những thao tác sau:
		- Cài full text search cho MS SQL Server cho thuộc tính TenChuyenDi trong bảng ChuyenDi và thuộc tính TenThanhVien trong bảng ThanhVien
		- Tạo CSDL bằng cách chạy file query "DBWeSplit.sql" trong thư mục Database
	* Nếu project này được dùng để làm demo cho khóa sau: Nhờ thầy chỉ cung cấp video demo và ẩn source code của nhóm em đi ạ.