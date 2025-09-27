using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PRN232_PROJECT_API.Migrations
{
    /// <inheritdoc />
    public partial class update_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "26ade076-eacf-4b38-b4e9-8974a12cd7b9", 0, "635f7130-e269-4b0e-8809-825d1f15bd6f", "user4@mail.com", true, "User 4", false, null, "USER4@MAIL.COM", "USER4@MAIL.COM", "AQAAAAIAAYagAAAAEIDv8tYiZsXOcNMSSXydpFg7Rc+OhxcpYFiCTwIKg0NJMF8H+qlKZqypC5dS2LHM4g==", null, false, "bad8c9fa-c9cf-4df2-b26a-b5961327b468", false, "user4@mail.com" },
                    { "30b0f7fc-af44-4535-9884-f601ecf3c542", 0, "373589af-f723-4c4f-b706-742cf6453bb7", "user1@mail.com", true, "User 1", false, null, "USER1@MAIL.COM", "USER1@MAIL.COM", "AQAAAAIAAYagAAAAEIrGx7vhiI5W2VX/N54X4fbBKo/edmmvx9yRPUH9OJU+wrrBxdFd5sA3CTIIZP6c/Q==", null, false, "628a80f0-4f5b-4f01-9cf6-2a98855a05dc", false, "user1@mail.com" },
                    { "30f6a7ea-7fbb-4868-8bdc-ef830fd9b524", 0, "ca72f80f-d8fd-40d3-9380-97c9741e7b80", "user3@mail.com", true, "User 3", false, null, "USER3@MAIL.COM", "USER3@MAIL.COM", "AQAAAAIAAYagAAAAECmYxLxaLboULkMMKJcUAVblsh/hTmNDlh8TuVPDgoHJSl6tcToaQIKy5eqx4vgRYg==", null, false, "1d30e0d7-7fb9-4c97-bb0c-b5c7a88bb9d4", false, "user3@mail.com" },
                    { "3897286d-b6c1-49f7-99de-d1539425c0ae", 0, "81b0abf4-5497-4c6a-aea2-7a6636920205", "user10@mail.com", true, "User 10", false, null, "USER10@MAIL.COM", "USER10@MAIL.COM", "AQAAAAIAAYagAAAAEMqJCXUFInKjBaAUKlHpCPydLh8Wm5/2dFDBYk7jV5SaceCd5B7nchUga0h2uGCr/g==", null, false, "b9f83d3b-6a97-4bec-8a8f-7bc840008ed4", false, "user10@mail.com" },
                    { "3d954c97-a30d-4303-a33d-f979be10d117", 0, "233bf43d-460d-49e3-82c9-e2bcf82e6a38", "user8@mail.com", true, "User 8", false, null, "USER8@MAIL.COM", "USER8@MAIL.COM", "AQAAAAIAAYagAAAAEKTfBao9fw1FkZ44iM/uwPfceFWd1MWJZDjfaj5tfk5UZebDPbMBO0FivVzWm05xvQ==", null, false, "941e2aea-96fa-4bee-8aed-421b78dd8211", false, "user8@mail.com" },
                    { "504626e4-58f0-4857-b76c-432decc17ecf", 0, "e4fa001f-63d2-4921-a3ac-85d6574d2ca2", "user14@mail.com", true, "User 14", false, null, "USER14@MAIL.COM", "USER14@MAIL.COM", "AQAAAAIAAYagAAAAEDii+PYsFKdwuZEwnHg8u83JwfBy0eszwkWw5z7rY/Tpea4gtWDbOlenAO/ivkK/iA==", null, false, "067cef96-a376-4a85-821d-77f7ac24a8d2", false, "user14@mail.com" },
                    { "6e39dac9-6449-4526-9225-c3bed2642868", 0, "559c6e13-734b-48c9-bca7-39d99275860a", "user2@mail.com", true, "User 2", false, null, "USER2@MAIL.COM", "USER2@MAIL.COM", "AQAAAAIAAYagAAAAEAJgWe0iwybG48JR/vw26leKHWB/zsrvCQmAcrq92xx4wQECkAw48MrVP1w0OAWlIw==", null, false, "45b9e2bd-6629-42ba-9dbf-faaf0c149c06", false, "user2@mail.com" },
                    { "8298ffaf-de2d-45f3-99d8-48b42bcbda1e", 0, "18106f60-75fe-40ff-97ee-2bbe63beb925", "user6@mail.com", true, "User 6", false, null, "USER6@MAIL.COM", "USER6@MAIL.COM", "AQAAAAIAAYagAAAAEIFSQzZ7W0L73GfNSBSwDhEBYwEUHHAIVCD7BIsG4b4CHTzxM/RtDXI00WCc7hmNPg==", null, false, "5e8d5ac9-d467-457b-9f7b-4d63b39b855b", false, "user6@mail.com" },
                    { "a2071d7a-5ca7-4616-918f-d84d1d9d4253", 0, "6ede7b43-fa8d-4a31-8de9-9fd8495777d1", "user11@mail.com", true, "User 11", false, null, "USER11@MAIL.COM", "USER11@MAIL.COM", "AQAAAAIAAYagAAAAEJl5TuUbX9iYns9RPcWf6J+mu7tnmswQBOrtyWh1to75QgpDFn5/fOb5pYUr83qS7w==", null, false, "9f0a9001-c923-4773-bdde-ed4e9070d19c", false, "user11@mail.com" },
                    { "b8f3a210-3810-429d-8df3-178c593cdcab", 0, "bbfea742-fb78-4248-bc40-8d7916ff50c5", "user9@mail.com", true, "User 9", false, null, "USER9@MAIL.COM", "USER9@MAIL.COM", "AQAAAAIAAYagAAAAEBsqbjnxjECMxfVaZ0u7Bk3YujmHDQbwC60NkRH/XTT7gDhDXhOMqNyXFeFg+0u46w==", null, false, "11b1bd03-9f5f-4b95-bf08-0f0be724b51e", false, "user9@mail.com" },
                    { "bd393cfd-81a8-42ea-9bf5-e1a08403193a", 0, "98f76289-ee2b-4397-ad43-4d33beb3cb5c", "user5@mail.com", true, "User 5", false, null, "USER5@MAIL.COM", "USER5@MAIL.COM", "AQAAAAIAAYagAAAAEKvxuENk4I5+87biZKKB+kJp99dFlFfrNHTytziOvU9adKveKogQKF3QEfJ4vInK9w==", null, false, "f88eca44-c37c-4e4b-a231-1ba9307bf0f4", false, "user5@mail.com" },
                    { "c592ca20-3d6f-46af-a810-221b7660630b", 0, "2fd65b3f-54eb-4666-9351-79b173559c47", "user13@mail.com", true, "User 13", false, null, "USER13@MAIL.COM", "USER13@MAIL.COM", "AQAAAAIAAYagAAAAEP5U3v8VhspqDxabgp8vQQ9Y64EDLomCzSJnUEuaGnjAo/FOuH+46JX/0qyk2L9Xfw==", null, false, "5fc707be-ab21-4acf-b701-a57a6273c4a3", false, "user13@mail.com" },
                    { "f44d764b-c05a-4dd7-b160-d85fb7d7b8e2", 0, "c00aca21-1a55-46c4-921c-3dfaa2769398", "user12@mail.com", true, "User 12", false, null, "USER12@MAIL.COM", "USER12@MAIL.COM", "AQAAAAIAAYagAAAAEAwshjgpUUKVzvBedUXZMwnZBfmw2EG2FP2s/dYaCQRjpkTO7Yj5xg4+NiWHJngt/w==", null, false, "06d57047-d6be-4982-aed4-39efc172f330", false, "user12@mail.com" },
                    { "f853b113-f84c-4052-8629-b77e2b55954b", 0, "0aa8deda-3387-4334-b698-e175bd0b566f", "user7@mail.com", true, "User 7", false, null, "USER7@MAIL.COM", "USER7@MAIL.COM", "AQAAAAIAAYagAAAAEHzJq3Ud+KLUpaoGKeK4QYWEYb1E6dyrIki53tvuh8e/yzfP+Yx397hX+3CktYCz+g==", null, false, "8e2751fb-1e3a-4c5a-ae97-183f11f90d5f", false, "user7@mail.com" },
                    { "fc8ad219-5366-4c74-89be-9ed162aebdc5", 0, "3cfaa6a6-4895-4687-9149-a2d1503ccc4d", "user15@mail.com", true, "User 15", false, null, "USER15@MAIL.COM", "USER15@MAIL.COM", "AQAAAAIAAYagAAAAEJ4s6Rpf9RUGS2cfFuUIqzPRC3xbRQGwAceizSPmKbTm7AABtXSF3Wjj5spfqA6+rQ==", null, false, "37538026-2533-4b6b-9567-8b420b538a26", false, "user15@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Category 1" },
                    { 2, "Category 2" },
                    { 3, "Category 3" },
                    { 4, "Category 4" },
                    { 5, "Category 5" },
                    { 6, "Category 6" },
                    { 7, "Category 7" },
                    { 8, "Category 8" },
                    { 9, "Category 9" },
                    { 10, "Category 10" },
                    { 11, "Category 11" },
                    { 12, "Category 12" },
                    { 13, "Category 13" },
                    { 14, "Category 14" },
                    { 15, "Category 15" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Tag 1" },
                    { 2, "Tag 2" },
                    { 3, "Tag 3" },
                    { 4, "Tag 4" },
                    { 5, "Tag 5" },
                    { 6, "Tag 6" },
                    { 7, "Tag 7" },
                    { 8, "Tag 8" },
                    { 9, "Tag 9" },
                    { 10, "Tag 10" },
                    { 11, "Tag 11" },
                    { 12, "Tag 12" },
                    { 13, "Tag 13" },
                    { 14, "Tag 14" },
                    { 15, "Tag 15" }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedAt", "ImageUrl", "Source", "Status", "Title" },
                values: new object[,]
                {
                    { 1, 2, "Sample content for article 1", new DateTime(2025, 6, 27, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(775), "none", "VN", 1, "Article Title 1" },
                    { 2, 3, "Sample content for article 2", new DateTime(2025, 6, 26, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(792), "none", "VN", 1, "Article Title 2" },
                    { 3, 4, "Sample content for article 3", new DateTime(2025, 6, 25, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(793), "none", "VN", 1, "Article Title 3" },
                    { 4, 5, "Sample content for article 4", new DateTime(2025, 6, 24, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(794), "none", "VN", 1, "Article Title 4" },
                    { 5, 6, "Sample content for article 5", new DateTime(2025, 6, 23, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(795), "none", "VN", 1, "Article Title 5" },
                    { 6, 7, "Sample content for article 6", new DateTime(2025, 6, 22, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(796), "none", "VN", 1, "Article Title 6" },
                    { 7, 8, "Sample content for article 7", new DateTime(2025, 6, 21, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(798), "none", "VN", 1, "Article Title 7" },
                    { 8, 9, "Sample content for article 8", new DateTime(2025, 6, 20, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(799), "none", "VN", 1, "Article Title 8" },
                    { 9, 10, "Sample content for article 9", new DateTime(2025, 6, 19, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(800), "none", "VN", 1, "Article Title 9" },
                    { 10, 11, "Sample content for article 10", new DateTime(2025, 6, 18, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(801), "none", "VN", 1, "Article Title 10" },
                    { 11, 12, "Sample content for article 11", new DateTime(2025, 6, 17, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(802), "none", "VN", 1, "Article Title 11" },
                    { 12, 13, "Sample content for article 12", new DateTime(2025, 6, 16, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(804), "none", "VN", 1, "Article Title 12" },
                    { 13, 14, "Sample content for article 13", new DateTime(2025, 6, 15, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(805), "none", "VN", 1, "Article Title 13" },
                    { 14, 15, "Sample content for article 14", new DateTime(2025, 6, 14, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(807), "none", "VN", 1, "Article Title 14" },
                    { 15, 1, "Sample content for article 15", new DateTime(2025, 6, 13, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(808), "none", "VN", 1, "Article Title 15" }
                });

            migrationBuilder.InsertData(
                table: "ArticleTags",
                columns: new[] { "ArticleId", "TagId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 3 },
                    { 3, 4 },
                    { 4, 5 },
                    { 5, 6 },
                    { 6, 7 },
                    { 7, 8 },
                    { 8, 9 },
                    { 9, 10 },
                    { 10, 11 },
                    { 11, 12 },
                    { 12, 13 },
                    { 13, 14 },
                    { 14, 15 },
                    { 15, 1 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "Content", "PostedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 2, "Comment 1 content.", new DateTime(2025, 6, 28, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(1899), "6e39dac9-6449-4526-9225-c3bed2642868" },
                    { 2, 3, "Comment 2 content.", new DateTime(2025, 6, 28, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(1906), "30f6a7ea-7fbb-4868-8bdc-ef830fd9b524" },
                    { 3, 4, "Comment 3 content.", new DateTime(2025, 6, 28, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(1908), "26ade076-eacf-4b38-b4e9-8974a12cd7b9" },
                    { 4, 5, "Comment 4 content.", new DateTime(2025, 6, 28, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(1911), "bd393cfd-81a8-42ea-9bf5-e1a08403193a" },
                    { 5, 6, "Comment 5 content.", new DateTime(2025, 6, 28, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(1913), "8298ffaf-de2d-45f3-99d8-48b42bcbda1e" },
                    { 6, 7, "Comment 6 content.", new DateTime(2025, 6, 28, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(1919), "f853b113-f84c-4052-8629-b77e2b55954b" },
                    { 7, 8, "Comment 7 content.", new DateTime(2025, 6, 28, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(1921), "3d954c97-a30d-4303-a33d-f979be10d117" },
                    { 8, 9, "Comment 8 content.", new DateTime(2025, 6, 28, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(1923), "b8f3a210-3810-429d-8df3-178c593cdcab" },
                    { 9, 10, "Comment 9 content.", new DateTime(2025, 6, 28, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(1947), "3897286d-b6c1-49f7-99de-d1539425c0ae" },
                    { 10, 11, "Comment 10 content.", new DateTime(2025, 6, 28, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(1951), "a2071d7a-5ca7-4616-918f-d84d1d9d4253" },
                    { 11, 12, "Comment 11 content.", new DateTime(2025, 6, 28, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(1953), "f44d764b-c05a-4dd7-b160-d85fb7d7b8e2" },
                    { 12, 13, "Comment 12 content.", new DateTime(2025, 6, 28, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(1955), "c592ca20-3d6f-46af-a810-221b7660630b" },
                    { 13, 14, "Comment 13 content.", new DateTime(2025, 6, 28, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(1957), "504626e4-58f0-4857-b76c-432decc17ecf" },
                    { 14, 15, "Comment 14 content.", new DateTime(2025, 6, 28, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(1963), "fc8ad219-5366-4c74-89be-9ed162aebdc5" },
                    { 15, 1, "Comment 15 content.", new DateTime(2025, 6, 28, 16, 20, 58, 278, DateTimeKind.Utc).AddTicks(1986), "30b0f7fc-af44-4535-9884-f601ecf3c542" }
                });

            migrationBuilder.InsertData(
                table: "UserArticles",
                columns: new[] { "ArticleId", "UserId", "RoleInArticle" },
                values: new object[,]
                {
                    { 4, "26ade076-eacf-4b38-b4e9-8974a12cd7b9", "MainAuthor" },
                    { 1, "30b0f7fc-af44-4535-9884-f601ecf3c542", "MainAuthor" },
                    { 3, "30f6a7ea-7fbb-4868-8bdc-ef830fd9b524", "MainAuthor" },
                    { 10, "3897286d-b6c1-49f7-99de-d1539425c0ae", "MainAuthor" },
                    { 8, "3d954c97-a30d-4303-a33d-f979be10d117", "MainAuthor" },
                    { 2, "6e39dac9-6449-4526-9225-c3bed2642868", "MainAuthor" },
                    { 6, "8298ffaf-de2d-45f3-99d8-48b42bcbda1e", "MainAuthor" },
                    { 9, "b8f3a210-3810-429d-8df3-178c593cdcab", "MainAuthor" },
                    { 5, "bd393cfd-81a8-42ea-9bf5-e1a08403193a", "MainAuthor" },
                    { 7, "f853b113-f84c-4052-8629-b77e2b55954b", "MainAuthor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ArticleTags",
                keyColumns: new[] { "ArticleId", "TagId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ArticleTags",
                keyColumns: new[] { "ArticleId", "TagId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "ArticleTags",
                keyColumns: new[] { "ArticleId", "TagId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "ArticleTags",
                keyColumns: new[] { "ArticleId", "TagId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "ArticleTags",
                keyColumns: new[] { "ArticleId", "TagId" },
                keyValues: new object[] { 5, 6 });

            migrationBuilder.DeleteData(
                table: "ArticleTags",
                keyColumns: new[] { "ArticleId", "TagId" },
                keyValues: new object[] { 6, 7 });

            migrationBuilder.DeleteData(
                table: "ArticleTags",
                keyColumns: new[] { "ArticleId", "TagId" },
                keyValues: new object[] { 7, 8 });

            migrationBuilder.DeleteData(
                table: "ArticleTags",
                keyColumns: new[] { "ArticleId", "TagId" },
                keyValues: new object[] { 8, 9 });

            migrationBuilder.DeleteData(
                table: "ArticleTags",
                keyColumns: new[] { "ArticleId", "TagId" },
                keyValues: new object[] { 9, 10 });

            migrationBuilder.DeleteData(
                table: "ArticleTags",
                keyColumns: new[] { "ArticleId", "TagId" },
                keyValues: new object[] { 10, 11 });

            migrationBuilder.DeleteData(
                table: "ArticleTags",
                keyColumns: new[] { "ArticleId", "TagId" },
                keyValues: new object[] { 11, 12 });

            migrationBuilder.DeleteData(
                table: "ArticleTags",
                keyColumns: new[] { "ArticleId", "TagId" },
                keyValues: new object[] { 12, 13 });

            migrationBuilder.DeleteData(
                table: "ArticleTags",
                keyColumns: new[] { "ArticleId", "TagId" },
                keyValues: new object[] { 13, 14 });

            migrationBuilder.DeleteData(
                table: "ArticleTags",
                keyColumns: new[] { "ArticleId", "TagId" },
                keyValues: new object[] { 14, 15 });

            migrationBuilder.DeleteData(
                table: "ArticleTags",
                keyColumns: new[] { "ArticleId", "TagId" },
                keyValues: new object[] { 15, 1 });

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "UserArticles",
                keyColumns: new[] { "ArticleId", "UserId" },
                keyValues: new object[] { 4, "26ade076-eacf-4b38-b4e9-8974a12cd7b9" });

            migrationBuilder.DeleteData(
                table: "UserArticles",
                keyColumns: new[] { "ArticleId", "UserId" },
                keyValues: new object[] { 1, "30b0f7fc-af44-4535-9884-f601ecf3c542" });

            migrationBuilder.DeleteData(
                table: "UserArticles",
                keyColumns: new[] { "ArticleId", "UserId" },
                keyValues: new object[] { 3, "30f6a7ea-7fbb-4868-8bdc-ef830fd9b524" });

            migrationBuilder.DeleteData(
                table: "UserArticles",
                keyColumns: new[] { "ArticleId", "UserId" },
                keyValues: new object[] { 10, "3897286d-b6c1-49f7-99de-d1539425c0ae" });

            migrationBuilder.DeleteData(
                table: "UserArticles",
                keyColumns: new[] { "ArticleId", "UserId" },
                keyValues: new object[] { 8, "3d954c97-a30d-4303-a33d-f979be10d117" });

            migrationBuilder.DeleteData(
                table: "UserArticles",
                keyColumns: new[] { "ArticleId", "UserId" },
                keyValues: new object[] { 2, "6e39dac9-6449-4526-9225-c3bed2642868" });

            migrationBuilder.DeleteData(
                table: "UserArticles",
                keyColumns: new[] { "ArticleId", "UserId" },
                keyValues: new object[] { 6, "8298ffaf-de2d-45f3-99d8-48b42bcbda1e" });

            migrationBuilder.DeleteData(
                table: "UserArticles",
                keyColumns: new[] { "ArticleId", "UserId" },
                keyValues: new object[] { 9, "b8f3a210-3810-429d-8df3-178c593cdcab" });

            migrationBuilder.DeleteData(
                table: "UserArticles",
                keyColumns: new[] { "ArticleId", "UserId" },
                keyValues: new object[] { 5, "bd393cfd-81a8-42ea-9bf5-e1a08403193a" });

            migrationBuilder.DeleteData(
                table: "UserArticles",
                keyColumns: new[] { "ArticleId", "UserId" },
                keyValues: new object[] { 7, "f853b113-f84c-4052-8629-b77e2b55954b" });

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "26ade076-eacf-4b38-b4e9-8974a12cd7b9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "30b0f7fc-af44-4535-9884-f601ecf3c542");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "30f6a7ea-7fbb-4868-8bdc-ef830fd9b524");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3897286d-b6c1-49f7-99de-d1539425c0ae");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3d954c97-a30d-4303-a33d-f979be10d117");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "504626e4-58f0-4857-b76c-432decc17ecf");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6e39dac9-6449-4526-9225-c3bed2642868");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8298ffaf-de2d-45f3-99d8-48b42bcbda1e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2071d7a-5ca7-4616-918f-d84d1d9d4253");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b8f3a210-3810-429d-8df3-178c593cdcab");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd393cfd-81a8-42ea-9bf5-e1a08403193a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c592ca20-3d6f-46af-a810-221b7660630b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f44d764b-c05a-4dd7-b160-d85fb7d7b8e2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f853b113-f84c-4052-8629-b77e2b55954b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fc8ad219-5366-4c74-89be-9ed162aebdc5");

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}
