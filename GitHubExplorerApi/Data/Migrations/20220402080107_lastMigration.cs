using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GitHubExplorerApi.data.migrations
{
    public partial class lastMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Licenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    key = table.Column<string>(type: "TEXT", nullable: true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    spdx_id = table.Column<string>(type: "TEXT", nullable: true),
                    url = table.Column<string>(type: "TEXT", nullable: true),
                    node_id = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    login = table.Column<string>(type: "TEXT", nullable: true),
                    node_id = table.Column<string>(type: "TEXT", nullable: true),
                    avatar_url = table.Column<string>(type: "TEXT", nullable: true),
                    gravatar_id = table.Column<string>(type: "TEXT", nullable: true),
                    url = table.Column<string>(type: "TEXT", nullable: true),
                    html_url = table.Column<string>(type: "TEXT", nullable: true),
                    followers_url = table.Column<string>(type: "TEXT", nullable: true),
                    following_url = table.Column<string>(type: "TEXT", nullable: true),
                    gists_url = table.Column<string>(type: "TEXT", nullable: true),
                    starred_url = table.Column<string>(type: "TEXT", nullable: true),
                    subscriptions_url = table.Column<string>(type: "TEXT", nullable: true),
                    organizations_url = table.Column<string>(type: "TEXT", nullable: true),
                    repos_url = table.Column<string>(type: "TEXT", nullable: true),
                    events_url = table.Column<string>(type: "TEXT", nullable: true),
                    received_events_url = table.Column<string>(type: "TEXT", nullable: true),
                    type = table.Column<string>(type: "TEXT", nullable: true),
                    site_admin = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "BLOB", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GitHubRepositories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GitHubId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    AvatarUrl = table.Column<string>(type: "TEXT", nullable: true),
                    node_id = table.Column<string>(type: "TEXT", nullable: true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    full_name = table.Column<string>(type: "TEXT", nullable: true),
                    @private = table.Column<bool>(name: "private", type: "INTEGER", nullable: true),
                    ownerId = table.Column<int>(type: "INTEGER", nullable: false),
                    html_url = table.Column<string>(type: "TEXT", nullable: true),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    fork = table.Column<bool>(type: "INTEGER", nullable: true),
                    url = table.Column<string>(type: "TEXT", nullable: true),
                    forks_url = table.Column<string>(type: "TEXT", nullable: true),
                    keys_url = table.Column<string>(type: "TEXT", nullable: true),
                    collaborators_url = table.Column<string>(type: "TEXT", nullable: true),
                    teams_url = table.Column<string>(type: "TEXT", nullable: true),
                    hooks_url = table.Column<string>(type: "TEXT", nullable: true),
                    issue_events_url = table.Column<string>(type: "TEXT", nullable: true),
                    events_url = table.Column<string>(type: "TEXT", nullable: true),
                    assignees_url = table.Column<string>(type: "TEXT", nullable: true),
                    branches_url = table.Column<string>(type: "TEXT", nullable: true),
                    tags_url = table.Column<string>(type: "TEXT", nullable: true),
                    blobs_url = table.Column<string>(type: "TEXT", nullable: true),
                    git_tags_url = table.Column<string>(type: "TEXT", nullable: true),
                    git_refs_url = table.Column<string>(type: "TEXT", nullable: true),
                    trees_url = table.Column<string>(type: "TEXT", nullable: true),
                    statuses_url = table.Column<string>(type: "TEXT", nullable: true),
                    languages_url = table.Column<string>(type: "TEXT", nullable: true),
                    stargazers_url = table.Column<string>(type: "TEXT", nullable: true),
                    contributors_url = table.Column<string>(type: "TEXT", nullable: true),
                    subscribers_url = table.Column<string>(type: "TEXT", nullable: true),
                    subscription_url = table.Column<string>(type: "TEXT", nullable: true),
                    commits_url = table.Column<string>(type: "TEXT", nullable: true),
                    git_commits_url = table.Column<string>(type: "TEXT", nullable: true),
                    comments_url = table.Column<string>(type: "TEXT", nullable: true),
                    issue_comment_url = table.Column<string>(type: "TEXT", nullable: true),
                    contents_url = table.Column<string>(type: "TEXT", nullable: true),
                    compare_url = table.Column<string>(type: "TEXT", nullable: true),
                    merges_url = table.Column<string>(type: "TEXT", nullable: true),
                    archive_url = table.Column<string>(type: "TEXT", nullable: true),
                    downloads_url = table.Column<string>(type: "TEXT", nullable: true),
                    issues_url = table.Column<string>(type: "TEXT", nullable: true),
                    pulls_url = table.Column<string>(type: "TEXT", nullable: true),
                    milestones_url = table.Column<string>(type: "TEXT", nullable: true),
                    notifications_url = table.Column<string>(type: "TEXT", nullable: true),
                    labels_url = table.Column<string>(type: "TEXT", nullable: true),
                    releases_url = table.Column<string>(type: "TEXT", nullable: true),
                    deployments_url = table.Column<string>(type: "TEXT", nullable: true),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: true),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: true),
                    pushed_at = table.Column<DateTime>(type: "TEXT", nullable: true),
                    git_url = table.Column<string>(type: "TEXT", nullable: true),
                    ssh_url = table.Column<string>(type: "TEXT", nullable: true),
                    clone_url = table.Column<string>(type: "TEXT", nullable: true),
                    svn_url = table.Column<string>(type: "TEXT", nullable: true),
                    homepage = table.Column<string>(type: "TEXT", nullable: true),
                    size = table.Column<int>(type: "INTEGER", nullable: true),
                    stargazers_count = table.Column<int>(type: "INTEGER", nullable: true),
                    watchers_count = table.Column<int>(type: "INTEGER", nullable: true),
                    language = table.Column<string>(type: "TEXT", nullable: true),
                    has_issues = table.Column<bool>(type: "INTEGER", nullable: true),
                    has_projects = table.Column<bool>(type: "INTEGER", nullable: true),
                    has_downloads = table.Column<bool>(type: "INTEGER", nullable: true),
                    has_wiki = table.Column<bool>(type: "INTEGER", nullable: true),
                    has_pages = table.Column<bool>(type: "INTEGER", nullable: true),
                    forks_count = table.Column<int>(type: "INTEGER", nullable: true),
                    mirror_url = table.Column<string>(type: "TEXT", nullable: true),
                    archived = table.Column<bool>(type: "INTEGER", nullable: true),
                    disabled = table.Column<bool>(type: "INTEGER", nullable: true),
                    open_issues_count = table.Column<int>(type: "INTEGER", nullable: true),
                    licenseId = table.Column<int>(type: "INTEGER", nullable: true),
                    allow_forking = table.Column<bool>(type: "INTEGER", nullable: true),
                    is_template = table.Column<bool>(type: "INTEGER", nullable: true),
                    visibility = table.Column<string>(type: "TEXT", nullable: true),
                    forks = table.Column<int>(type: "INTEGER", nullable: true),
                    open_issues = table.Column<int>(type: "INTEGER", nullable: true),
                    watchers = table.Column<int>(type: "INTEGER", nullable: true),
                    default_branch = table.Column<string>(type: "TEXT", nullable: true),
                    score = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GitHubRepositories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GitHubRepositories_Licenses_licenseId",
                        column: x => x.licenseId,
                        principalTable: "Licenses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GitHubRepositories_Owners_ownerId",
                        column: x => x.ownerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GitHubRepositories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GitHubRepositories_licenseId",
                table: "GitHubRepositories",
                column: "licenseId");

            migrationBuilder.CreateIndex(
                name: "IX_GitHubRepositories_ownerId",
                table: "GitHubRepositories",
                column: "ownerId");

            migrationBuilder.CreateIndex(
                name: "IX_GitHubRepositories_UserId",
                table: "GitHubRepositories",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GitHubRepositories");

            migrationBuilder.DropTable(
                name: "Licenses");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
