create database framework;
use framework;

CREATE TABLE ACCOUNT
(
	UserID int identity(1,1) NOT NULL PRIMARY KEY,
	UName varchar(20),
	UPass varchar(30),
	UType int,
	UStatus int,
	UEmail varchar(100),
	UPhone varchar(50),
	UAva varchar(MAX)
	

);
CREATE TABLE POST
(
	PostID int identity(1,1) NOT NULL PRIMARY KEY,
	PUserID int,
	PTitle NVARCHAR(MAX),
	PPrice int,
	Heart int,
	PStatus int,
	PDate datetime,
	PDesc NVARCHAR(MAX)
	CONSTRAINT FK_P_AC FOREIGN KEY (PUserID) REFERENCES ACCOUNT(UserID)
);


CREATE TABLE PICTURE
(
	ImageID int identity(1,1) NOT NULL PRIMARY KEY,
	IPostID int,
	ILink varchar(MAX),
	IOrder int,
	CONSTRAINT FK_IM_P FOREIGN KEY (IPostID) REFERENCES POST(PostID)
);


--Xem xét lại bảng này, có nên làm chức năng noti không ay chuyển thằng qua email-
CREATE TABLE NOTI
(
	NotiID int identity(1,1) NOT NULL PRIMARY KEY,
	NUserID int,
	NDate datetime,
	NContent nvarchar(MAX),
	CONSTRAINT FK_NO_AC FOREIGN KEY (NUserID) REFERENCES ACCOUNT(UserID)
);
------------------------


CREATE TABLE BAN
(
	BanID int identity(1,1) NOT NULL PRIMARY KEY,
	BedUserID int, 
	BUserID int,
	BDate datetime,
	BReason nvarchar(MAX),
	FreeDate datetime,
	CONSTRAINT FK_BA_AC FOREIGN KEY (BUserID) REFERENCES ACCOUNT(UserID),
	CONSTRAINT FK2_BA_AC FOREIGN KEY (BedUserID) REFERENCES ACCOUNT(UserID)
);
CREATE TABLE REPORT
(
	ReportID int identity(1,1) NOT NULL PRIMARY KEY,
	RedUserID int,  
	RUserID int,
	RReason nvarchar(MAX),
	RDate datetime,
	CONSTRAINT FK_RE_AC FOREIGN KEY (RUserID) REFERENCES ACCOUNT(UserID),
	CONSTRAINT FK2_RE_AC FOREIGN KEY (RedUserID) REFERENCES ACCOUNT(UserID)
);

CREATE TABLE COMMENT
(
	CommentID int identity(1,1) NOT NULL PRIMARY KEY,
	CPostID int,
	CUserID int,
	CContent nvarchar(MAX),
	CPrice int,
	CDate datetime,
	CONSTRAINT FK_CO_AC FOREIGN KEY (CUserID) REFERENCES ACCOUNT(UserID),
	CONSTRAINT FK2_CO_P FOREIGN KEY (CPostID) REFERENCES POST(PostID)
);


CREATE TABLE INTEREST
(
	InPostID int NOT NULL, 
	InUserID int NOT NULL,
	InDate datetime,
	CONSTRAINT FK_IN_AC FOREIGN KEY (InUserID) REFERENCES ACCOUNT(UserID),
	CONSTRAINT FK2_IN_P FOREIGN KEY (InPostID) REFERENCES POST(PostID)
	
);
ALTER TABLE INTEREST ADD CONSTRAINT PriKeyInterest PRIMARY KEY (InPostID, InUserID);
CREATE TABLE CHAT
(
	ChatID int identity(1,1) NOT NULL PRIMARY KEY,
	ChUserID int,
	ChDate varchar(25),
	ChContent nvarchar(MAX),
	CONSTRAINT FK_CH_AC FOREIGN KEY (ChUserID) REFERENCES ACCOUNT(UserID)
);

select * from account

INSERT INTO account VALUES ('TrucXinhDep','trucxinh123',0,1,'trinhthithanhtruc1831@gmail.com','792937453','/public/assets/ava/default.png');
INSERT INTO account VALUES ('CoGaiMuaDong199x','cogaimuadong2k2',0,1,'cogaimuadong2k2@gmail.com','908857244','/public/assets/ava/default.png');
INSERT INTO account VALUES ('Admin1','Admin123',1,1,'19521059@gm.uit.edu.vn','812584587','/public/assets/ava/default.png');
INSERT INTO account VALUES ('TienSiMyThuat','tiensi123',0,1,'minhnhien1831@gmail.com','857773562','/public/assets/ava/default.png');
INSERT INTO account VALUES ('NhoxChungTinh1831','ncting123',0,1,'alonelyfish521059@gmail.com','867597129','/public/assets/ava/default.png');
INSERT INTO account VALUES ('P3HeoMaiLuvAnk','peheo123',0,1,'chungtalanhungconbo588@gmail.com','877420696','/public/assets/ava/default.png');
INSERT INTO account VALUES ('LangTuSongGiong','lantu123',0,1,'samurai8vn@gmail.com','887244263','/public/assets/ava/default.png');
INSERT INTO account VALUES ('NhanGiaLangLa','nhangia123',0,1,'narutohommie@gmail.com','897067830','/public/assets/ava/default.png');
INSERT INTO account VALUES ('MaiKimTri','mktri123',0,1,'maikimtri1831@gmail.com','906891397','/public/assets/ava/default.png');

INSERT INTO chat VALUES ('4','[2021-12-29 22:59:33] ',N'Có ai biết cái đèn dầu mà dạng có bóng thủy tinh, có cái tim đèn bằng dây không?');
INSERT INTO chat VALUES ('5','[2021-12-29 23:00:00] ',N'Hình như t từng thấy có người đăng bài này r');
INSERT INTO chat VALUES ('8','[2021-12-29 23:01:03] ',N'Đúng r t cũng từng thấy r, cậu search thử xem');
select * from post
INSERT INTO post VALUES (1,N'Dĩa sứ 50 năm tuổi họa tiết hoa văn xanh đỏ',1000000,0,1,'2021-12-08 22:59:33',N'Đĩa sứ với họa tiết hoa xanh đỏ với số tuổi lên đến 50, không bị sứt mẻ, hình in có vài chỗ đã bị phai nhưng tổng thể mang lại hơi hướng vintage.');
INSERT INTO post VALUES (1,N'Chén sắt 75 năm tuổi họa tiết hoa văn đỏ',3000000,0,1,'2021-12-08 08:59:33',N'Chén sắt từ những năm 45s được xem là báu vật gia đình nếu nhà nào vẫn còn giữ. Tuy đã bị sờn cũ nhưng nó mang lại ký ức về thời Pháp thuộc.');
INSERT INTO post VALUES (1,N'Dĩa sứ 20 năm tuổi họa tiết hoa văn đỏ',500000,0,1,'2021-12-08 22:59:33',N'Đĩa sứ họa tiết hoa văn đỏ, đơn giản nhưng tinh tế với 3 họa tiết dàn hoa đỏ vàng được chia đều trên thành đĩa, tọa cảm giác cân xứng hài hòa.');
INSERT INTO post VALUES (4,N'Chén sứ cổ triều Hồ phiên bản Thọ',1000000,0,1,'2021-03-20 06:59:30',N'Chén sứ cổ triều Nguyễn phiên bản Thọ được lưu truyền qua bao đời, được gìn giữ cẩn thận, hoa văn trên chén rõ nét không bị phai mờ.');
INSERT INTO post VALUES (7,N'Hộp đựng chỉ của bà nội tôi!! Mua để xin vía may đẹp như bà nội!!!',750000,0,1,'2021-06-08 02:59:12',N'Nhìn vào các bạn có thể nghĩ đây là chiêc hộp bánh rỉ sét nhưng nó chính là chiếc hộp bánh bà nội tận dụng làm hộp đựng kim chỉ, tuy đã cũ nhưng nó chứa chan tình thương bà nội, tuổi thơ của bao người.');
INSERT INTO post VALUES (4,N'Cối đá Tây Nguyên!! Không bao gồm chày!!',500000,0,1,'2021-12-01 22:06:50',N'Cối đá Tây Nguyên được đẽo ra từ tảng đá nguyên khối nặng 2kg, không bao gồm chày, đảm bảo 100% giã không vỡ cối, đồ ăn ngon hơn nhờ mang hương vị đá rừng.');
INSERT INTO post VALUES (7,N'Bán bàn ủi sắt của mẹ!! Kẻ hủy diệt trang phục!!',250000,0,1,'2021-07-05 12:23:22',N'Chiếc bàn ủi có lẽ đã ngừng sản xuất từ lâu, đã trải qua bao năm là đồ nhưng vẫn dùng tốt, đồ nào nhăn khó ủi qua đây phẳng hơn cả màn hình tivi samsung siêu mỏng.');
INSERT INTO post VALUES (1,N'Đèn dầu cổ thời nhà Trần',400000,0,1,'2021-01-08 14:59:53',N'Đèn dầu cổ thời nhà Trần được lưu truyền qua bao đời, đèn vẫn sáng tốt với điều kiện có dầu hỏa, có thể giúp con bạn học bài lúc nhà cúp điện giúp cháu bé trở thành trạng nguyên.');
INSERT INTO post VALUES (6,N'Card Album HYLT!! Jisoo!!! Lisa!!! Rose!!',800000,0,1,'2021-12-08 18:59:03',N'BlackPink in your area!!! Card Album HYLT!! Jisoo!!! Lisa!!! Rose!! Nếu là một Blink chắc bạn không thể bỏ qua sản phẩm này, các cô gái hắc hường sẽ tới khu vực của bạn ngay khi bạn nhấn mua hàng.');
INSERT INTO post VALUES (9,N'Bức Dế Mèn Phiêu Lưu Ký của Danh họa TXD',12000000,0,1,'2021-12-20 23:06:40',N'Danh họa nổi tiếng hoặc cũng không TXD với bức tranh dế mèn không thể sáng tạo hơn, mặc dù đã trải qua thời gian nghỉ dưỡng trong góc nhà nhưng em nó vẫn còn tốt không bị chuột xé.');
INSERT INTO post VALUES (9,N'Bức Thỏ Nghệ Sĩ của Danh họa TXD',16000000,0,1,'2021-03-08 22:59:28',N'Vẫn là danh họa nổi tiếng hoặc cũng không TXD với bức vẽ Chú thỏ nghệ sĩ được dựa trên câu chuyện vượt khó để theo đuổi đam mê hội họa của nhân vật T.');
INSERT INTO post VALUES (9,N'Bức Hồ Thiên Nga của Danh họa TXD',20000000,0,1,'2021-05-08 22:59:33',N'Bức vẽ Hồ Thiên Nga theo phong cách "vẽ dán" chính những chú thiên nga nhưng mang hơi hướng con hạc đã tạo cho bức tranh nét độc đáo, cộng thêm những bông hoa đỏ rực bên bờ sông tạo điểm nổi bật cho bức tranh.');
INSERT INTO post VALUES (9,N'Bức Hoàng Hôn Cô Đơn Trong Tôi của Danh họa TXD',24000000,0,1,'2021-12-08 06:59:33',N'Bức vẽ thể hiện nội tâm đang giằng xé, bão tố sâu bên trong tâm hồn của một chàng trai. Trước biển rộng lớn với ánh hoàng hôn đơn côi chàng trai ấy không biết phải đối mặt với nó như nào.');
INSERT INTO post VALUES (9,N'Bộ siêu tập Thiên Thần Giáng Thế của Danh họa TXD',28000000,0,1,'2021-06-01 08:23:22',N'Bộ sưu tập những nàng tiên Winx xinh đẹp "Phép thuật Winx enchantix", mỗi nàng tiên đều mang màu sắc khác nhau, nhưng nó vẫn thể hiện được nét tinh nghịch của danh họa TXD');
INSERT INTO post VALUES (9,N'Tranh cổ tĩnh vực Sen và Ấm tích của Danh họa TXD',32000000,0,1,'2021-12-05 22:59:33',N'Danh họa nổi tiếng hoặc cũng không TXD đã thử sức với nhiều lĩnh vực và trong đó có bộ môn tranh tĩnh vật, chiếc ấm trà mang màu sắc tuổi thơ tươi vui bên 3 búp sen gợi nhớ đến thời cấp 2 ai cũng phải đối mặt với môn mỹ thuật.');
INSERT INTO post VALUES (9,N'Bức Mùa Xuân của Danh họa TXD',36000000,0,1,'2021-01-08 22:06:33',N'"Xuân đã đến bên em đến luôn bên tôi" xuân đã ngập tràn sắc trời, những chú chim có cặp có đôi, ai chưa có bồ thi mua tranh sẽ có đừng để xuân này đơn côi.');
INSERT INTO post VALUES (9,N'Bức Tĩnh vật Hoa Hồng tặng Em của Danh họa TXD',40000000,0,1,'2021-12-30 22:06:33',N'Bức tranh tĩnh vật bông hoa đang chống chọi với cơn mưa đạn nước, nhưng bạn có thể thấy hoa vẫn tươi xinh nên hãy đối mặt với vấn đề một cách tích cực rồi mọi chuyện sẽ được giải quyết.');
INSERT INTO post VALUES (9,N'Bức Bình minh chào Bình minh của Danh họa TXD',44000000,0,1,'2021-05-08 08:59:06',N'Mặt trời thức dậy như hòn lửa, cành cây chim ríu rít reo hò, có 2 cô gái đang chào tạm việt, có lẽ một đi không trở về ăn trưa vì cô kia mang theo hộp cơm bento.');




INSERT INTO picture VALUES (1,'/public/assets/product/1.jpg',1);
INSERT INTO picture VALUES (1,'/public/assets/product/2.jpg',2);
INSERT INTO picture VALUES (1,'/public/assets/product/3.jpg',3);
INSERT INTO picture VALUES (2,'/public/assets/product/4.jpg',1);
INSERT INTO picture VALUES (2,'/public/assets/product/5.jpg',2);
INSERT INTO picture VALUES (2,'/public/assets/product/6.jpg',3);
INSERT INTO picture VALUES (3,'/public/assets/product/7.jpg',1);
INSERT INTO picture VALUES (3,'/public/assets/product/8.jpg',2);
INSERT INTO picture VALUES (3,'/public/assets/product/9.jpg',3);
INSERT INTO picture VALUES (4,'/public/assets/product/10.jpg',1);
INSERT INTO picture VALUES (4,'/public/assets/product/11.jpg',2);
INSERT INTO picture VALUES (4,'/public/assets/product/12.jpg',3);
INSERT INTO picture VALUES (5,'/public/assets/product/13.jpg',1);
INSERT INTO picture VALUES (5,'/public/assets/product/14.jpg',2);
INSERT INTO picture VALUES (5,'/public/assets/product/15.jpg',3);
INSERT INTO picture VALUES (6,'/public/assets/product/16.jpg',1);
INSERT INTO picture VALUES (6,'/public/assets/product/17.jpg',2);
INSERT INTO picture VALUES (6,'/public/assets/product/18.jpg',3);
INSERT INTO picture VALUES (7,'/public/assets/product/19.jpg',1);
INSERT INTO picture VALUES (7,'/public/assets/product/20.jpg',2);
INSERT INTO picture VALUES (7,'/public/assets/product/21.jpg',3);
INSERT INTO picture VALUES (8,'/public/assets/product/22.jpg',1);
INSERT INTO picture VALUES (8,'/public/assets/product/23.jpg',2);
INSERT INTO picture VALUES (8,'/public/assets/product/24.jpg',3);
INSERT INTO picture VALUES (9,'/public/assets/product/25.jpg',1);
INSERT INTO picture VALUES (9,'/public/assets/product/26.jpg',2);
INSERT INTO picture VALUES (9,'/public/assets/product/27.jpg',3);
INSERT INTO picture VALUES (9,'/public/assets/product/28.jpg',4);
INSERT INTO picture VALUES (10,'/public/assets/product/29.jpg',1);
INSERT INTO picture VALUES (10,'/public/assets/product/30.jpg',2);
INSERT INTO picture VALUES (11,'/public/assets/product/31.jpg',1);
INSERT INTO picture VALUES (11,'/public/assets/product/32.jpg',2);
INSERT INTO picture VALUES (11,'/public/assets/product/33.jpg',3);
INSERT INTO picture VALUES (11,'/public/assets/product/34.jpg',4);
INSERT INTO picture VALUES (12,'/public/assets/product/35.jpg',1);
INSERT INTO picture VALUES (12,'/public/assets/product/36.jpg',2);
INSERT INTO picture VALUES (12,'/public/assets/product/37.jpg',3);
INSERT INTO picture VALUES (13,'/public/assets/product/38.jpg',1);
INSERT INTO picture VALUES (13,'/public/assets/product/39.jpg',2);
INSERT INTO picture VALUES (13,'/public/assets/product/40.jpg',3);
INSERT INTO picture VALUES (14,'/public/assets/product/41.jpg',1);
INSERT INTO picture VALUES (14,'/public/assets/product/42.jpg',2);
INSERT INTO picture VALUES (14,'/public/assets/product/43.jpg',3);
INSERT INTO picture VALUES (14,'/public/assets/product/44.jpg',4);
INSERT INTO picture VALUES (15,'/public/assets/product/45.jpg',1);
INSERT INTO picture VALUES (15,'/public/assets/product/46.jpg',2);
INSERT INTO picture VALUES (15,'/public/assets/product/47.jpg',3);
INSERT INTO picture VALUES (15,'/public/assets/product/48.jpg',4);
INSERT INTO picture VALUES (16,'/public/assets/product/49.jpg',1);
INSERT INTO picture VALUES (16,'/public/assets/product/50.jpg',2);
INSERT INTO picture VALUES (16,'/public/assets/product/51.jpg',3);
INSERT INTO picture VALUES (16,'/public/assets/product/52.jpg',4);
INSERT INTO picture VALUES (17,'/public/assets/product/53.jpg',1);
INSERT INTO picture VALUES (17,'/public/assets/product/54.jpg',2);
INSERT INTO picture VALUES (17,'/public/assets/product/55.jpg',3);
INSERT INTO picture VALUES (17,'/public/assets/product/56.jpg',4);
INSERT INTO picture VALUES (17,'/public/assets/product/57.jpg',5);
INSERT INTO picture VALUES (18,'/public/assets/product/58.jpg',1);
INSERT INTO picture VALUES (18,'/public/assets/product/59.jpg',2);
INSERT INTO picture VALUES (18,'/public/assets/product/60.jpg',3);

INSERT INTO interest VALUES (2,1,'2021-12-08 22:59:33');
INSERT INTO interest VALUES (8,1,'2021-12-10 03:22:41');
INSERT INTO interest VALUES (10,1,'2021-12-29 09:42:03');




