using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamTaskManager.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddDbSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Project_ProjectId",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignment_AspNetUsers_UserId",
                table: "TaskAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignment_Task_TaskId",
                table: "TaskAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProjects_Project_ProjectId",
                table: "UserProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskAssignment",
                table: "TaskAssignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Task",
                table: "Task");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                table: "Project");

            migrationBuilder.RenameTable(
                name: "TaskAssignment",
                newName: "TaskAssignments");

            migrationBuilder.RenameTable(
                name: "Task",
                newName: "Tasks");

            migrationBuilder.RenameTable(
                name: "Project",
                newName: "Projects");

            migrationBuilder.RenameIndex(
                name: "IX_TaskAssignment_UserId",
                table: "TaskAssignments",
                newName: "IX_TaskAssignments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Task_ProjectId",
                table: "Tasks",
                newName: "IX_Tasks_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskAssignments",
                table: "TaskAssignments",
                columns: new[] { "TaskId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignments_AspNetUsers_UserId",
                table: "TaskAssignments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignments_Tasks_TaskId",
                table: "TaskAssignments",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjects_Projects_ProjectId",
                table: "UserProjects",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignments_AspNetUsers_UserId",
                table: "TaskAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignments_Tasks_TaskId",
                table: "TaskAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProjects_Projects_ProjectId",
                table: "UserProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskAssignments",
                table: "TaskAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "Task");

            migrationBuilder.RenameTable(
                name: "TaskAssignments",
                newName: "TaskAssignment");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "Project");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_ProjectId",
                table: "Task",
                newName: "IX_Task_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskAssignments_UserId",
                table: "TaskAssignment",
                newName: "IX_TaskAssignment_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Task",
                table: "Task",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskAssignment",
                table: "TaskAssignment",
                columns: new[] { "TaskId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Project_ProjectId",
                table: "Task",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignment_AspNetUsers_UserId",
                table: "TaskAssignment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignment_Task_TaskId",
                table: "TaskAssignment",
                column: "TaskId",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjects_Project_ProjectId",
                table: "UserProjects",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
