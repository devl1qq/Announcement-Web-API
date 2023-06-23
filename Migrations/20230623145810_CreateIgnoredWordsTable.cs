using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Announcement_Web_API.Migrations
{
    /// <inheritdoc />
    public partial class CreateIgnoredWordsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IgnoredWords",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Word = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IgnoredWords", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "IgnoredWords",
                columns: new[] { "Id", "Word" },
                values: new object[,]
                {
                    { "023e4368-a877-4f55-92ff-ee2b3e9f8b52", "to" },
                    { "08ecc0b6-98d8-4bd4-87fd-0444048d9596", "you" },
                    { "124f6269-2e6d-45f4-b976-d396f77efa6c", "out" },
                    { "1324e1fc-457a-4920-ab5a-43b1a1dc086d", "can" },
                    { "148406a1-35a2-443b-b8e9-7c83c1cf7633", "no" },
                    { "14edd813-50f4-47f5-aaf2-66a975c8d2aa", "was" },
                    { "1d28de7c-d4ba-4973-afcb-77cb17ccef6b", "I" },
                    { "21a878f2-ce86-4c84-82fb-b76ea0f9c21c", "up" },
                    { "21e08a35-8f99-450a-99fe-ff007ee59105", "did" },
                    { "2449bb4e-2a5c-4427-9e60-cf6ee744df54", "they" },
                    { "2576b476-7e50-42f0-bad8-42b3c58bd0bd", "by" },
                    { "26715c57-5efc-462f-9a48-4b2ba040cc81", "down" },
                    { "2b5f8110-15cf-4730-b3d5-6e7a2c9f4a72", "far" },
                    { "2da521cc-ce25-49db-82ff-030c6ada1fce", "among" },
                    { "2e27076b-8632-4d13-a796-c2a69c5ab931", "will" },
                    { "2e8763db-1283-43c9-b58d-1efad0cd5d94", "his" },
                    { "2f2b5e9f-0261-488e-ad15-c304979f3bf5", "your" },
                    { "31297ed9-706b-40bc-89ef-be3ba8eee02c", "me" },
                    { "3940f0c9-1729-4639-8497-3f4e68432096", "my" },
                    { "39cfb9df-26fb-424f-bb22-5a5c2a7ef582", "have" },
                    { "3c08265f-aadd-40db-a552-f760d615a485", "us" },
                    { "3c16a614-9d4b-4485-86ef-2026b4e81c5c", "until" },
                    { "3c539c0e-fe78-4c7f-aff4-5ff7fded4b48", "ours" },
                    { "41e37a18-b437-493e-864f-781fbf00e3cd", "being" },
                    { "42e258f0-63e8-4fb7-a3a9-87d5f22c1d44", "into" },
                    { "48f22d16-ce74-4e43-b3d9-f2daea6fa4e0", "where" },
                    { "4df65a98-743a-44ed-9b1d-41951d25f7a4", "until" },
                    { "5099353d-42a5-485e-ac21-0272c30e729a", "should" },
                    { "50c5afd0-2749-4168-9364-98b89df2b949", "although" },
                    { "554225a2-bd3e-4a88-8ceb-f0a78b7a6943", "which" },
                    { "5ad32a45-c9ae-43a3-a60a-b4b0089a58a4", "the" },
                    { "5b0572a8-4a20-4b23-84ec-1db9425986e7", "she" },
                    { "5bb275ab-8aaf-44e1-9089-29314d2a1f62", "of" },
                    { "60041f67-13a1-46c3-b1cd-b8e80096e918", "nor" },
                    { "64fd5588-bb96-4319-ada4-2a52501b93b7", "as" },
                    { "696943f6-022b-4143-8c66-9160631a2d06", "through" },
                    { "696aef5a-0615-46fb-a2f4-8989193781b5", "during" },
                    { "6bc33ee2-2858-4a52-a377-fc7eaf9cbfab", "though" },
                    { "6be9e84e-d18b-4179-a3ec-a9f9ed7a04ec", "with" },
                    { "6e4c5afb-9ade-4f61-ad5a-d3f5d69b400c", "who" },
                    { "7208158b-3bef-40cf-95e1-aea2c22adb65", "under" },
                    { "73c866ac-ce0b-441a-b44a-365e9f79434e", "above" },
                    { "75aef800-af04-471f-8c72-fcf2152a07da", "we" },
                    { "75bb161e-8ef8-4aa1-b17b-b05d593a208b", "theirs" },
                    { "76475e1f-3ff8-4265-9ea8-70d7107dd7b6", "had" },
                    { "76e5850f-26b7-44c3-9e4e-8438198efc62", "on" },
                    { "77d0d531-09a0-4eda-ac0b-b25a50aaa567", "must" },
                    { "7ce02020-370e-4480-a660-576606a0656f", "mine" },
                    { "80b542de-2477-4d8f-9534-a133e6a91646", "them" },
                    { "828b39eb-33ff-47f9-ab78-a311389a6a9e", "whom" },
                    { "89f4f234-5aed-473b-9ad7-80ce23430db5", "he" },
                    { "8c197d44-17c1-4e7a-acc4-a3e061e29e34", "has" },
                    { "8cb0b894-17e9-495e-ad8b-6617142e38f1", "a" },
                    { "8cc57f9a-ee06-46cc-bea0-aff4ae4184fc", "hers" },
                    { "8d01ac2d-57b0-47da-b9cc-d3587a4f0a3b", "not" },
                    { "8fe08307-32bc-424e-93a4-0a321966faf7", "him" },
                    { "91334e99-7c3f-4307-b5f8-37b7549fc715", "before" },
                    { "92617879-4054-4e3a-bf48-e915678ad5bb", "shall" },
                    { "92b17138-8aab-46f4-b9ec-1480d3301cad", "below" },
                    { "92c88b57-3946-460c-b2ff-e4ad0151baf5", "how" },
                    { "92dcd6a4-3658-468e-81ac-dcb35a4e541a", "its" },
                    { "95dcbdcb-94f7-4c35-9ec3-9bfb080154ff", "it" },
                    { "970ee62c-9655-43e4-bac6-437bb20d154a", "their" },
                    { "9c6b0523-9761-4d96-b01c-4b50412ea623", "might" },
                    { "9cf00c95-6ff5-497f-b1f0-73a6eb293b59", "been" },
                    { "a4e2b630-47ce-48ab-a4c2-cc4b6d01b239", "while" },
                    { "a8272040-2a91-4d53-9fbf-fe7a86cf30bd", "near" },
                    { "a8f800ed-fd2f-444c-beda-2bc02da8eb01", "then" },
                    { "a94bfc81-dd8f-4171-ad15-7d3acabd0375", "at" },
                    { "ab13f878-b0ee-49d5-9555-3dbfa6daeef0", "when" },
                    { "abebf6e8-6b8d-4f9f-b63a-9d6cf9ddcfb3", "does" },
                    { "af7ba0b8-d700-4751-95e7-6084077d261f", "between" },
                    { "b99a4420-ea25-4276-92d7-5db7b7ea8d69", "in" },
                    { "bc28fcef-34f1-47bb-9d26-c7ffbff8ade9", "since" },
                    { "beecbd5f-633a-47dc-b5c6-ac779657a2d9", "were" },
                    { "bf9138a2-3342-4d52-8d9a-a1e51766cc04", "our" },
                    { "c01ea2da-3879-4753-8d24-9fc4144c49df", "because" },
                    { "c610c75a-4089-44a1-a8d2-0a2f45524b63", "beside" },
                    { "c74e24d7-b3e8-4816-9d45-59d8d634790f", "her" },
                    { "d5cff551-af94-464c-b1ad-4a66b7e7a834", "from" },
                    { "dab72b0d-31c4-44e7-8de3-7336dc3c715c", "if" },
                    { "dade1c30-7c18-4850-982c-fefe73705b9c", "else" },
                    { "de6c03d2-c0ad-4048-8f12-6c684d32313b", "could" },
                    { "df07541f-626a-4782-a239-5f223c615d83", "since" },
                    { "dffb45d8-c420-442a-bdf6-eeb83b861f6a", "unless" },
                    { "e247cf3d-b0f6-4ce5-80db-9697eb6534c5", "do" },
                    { "e4efcd45-d8f2-4117-b660-195895c30f1c", "what" },
                    { "e79d529d-076e-44d9-903b-40a081be92a0", "after" },
                    { "e8f30cb5-b7c1-4b2c-b16a-b9852a96222f", "over" },
                    { "eac27162-987b-4f9e-9201-763d2d9e6322", "be" },
                    { "eccabc98-85a2-4d15-a40c-f5a8544f64af", "why" },
                    { "ee8b2f66-cc68-4c0b-bc9a-8527fe70ed83", "may" },
                    { "eecf65b5-8f7d-4f97-879b-effe8fa6a3e3", "whether" },
                    { "ef571424-1e87-4ac4-b79e-35b4a423bf34", "whose" },
                    { "f0d16528-de8f-46b3-a71d-ce401c9f4338", "is" },
                    { "f1d8bd0e-43b1-4499-98df-0c12a7c50aa9", "an" },
                    { "fc649cfe-c423-4e77-a89b-80170930e75b", "would" },
                    { "fef872ee-f26f-46ce-bdc4-16ba865ec049", "for" },
                    { "ff7e8380-4add-416d-b217-d0e367f1a2b7", "are" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IgnoredWords");
        }
    }
}
