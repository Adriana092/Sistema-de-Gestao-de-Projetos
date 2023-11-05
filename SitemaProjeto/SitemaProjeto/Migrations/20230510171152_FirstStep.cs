using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SitemaProjeto.Migrations
{
    /// <inheritdoc />
    public partial class FirstStep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    ID_Aluno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Curso = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Numero_Mec = table.Column<int>(type: "int", nullable: false),
                    Pass = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Aluno__06D5E4761F943BAA", x => x.ID_Aluno);
                });

            migrationBuilder.CreateTable(
                name: "AlunoGrupo",
                columns: table => new
                {
                    IdAluno = table.Column<int>(type: "int", nullable: false),
                    IdGrupo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoGrupo", x => new { x.IdAluno, x.IdGrupo });
                });

            migrationBuilder.CreateTable(
                name: "Edicao",
                columns: table => new
                {
                    ID_Edicao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Descricao = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    Valido_de = table.Column<int>(type: "int", nullable: false),
                    Valido_ate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Edicao__FB4794052FD7C2F0", x => x.ID_Edicao);
                });

            migrationBuilder.CreateTable(
                name: "Orientador",
                columns: table => new
                {
                    ID_Orientador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Instituicao = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Pass = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orientad__C15B3CC7AA06E5C6", x => x.ID_Orientador);
                });

            migrationBuilder.CreateTable(
                name: "Grupo",
                columns: table => new
                {
                    ID_Grupo = table.Column<int>(type: "int", nullable: false),
                    ID_Edicao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Grupo__886D5DEECDF1CD75", x => x.ID_Grupo);
                    table.ForeignKey(
                        name: "FK__Grupo__ID_Edicao__33D4B598",
                        column: x => x.ID_Edicao,
                        principalTable: "Edicao",
                        principalColumn: "ID_Edicao");
                    table.ForeignKey(
                        name: "FK__Grupo__ID_Grupo__34C8D9D1",
                        column: x => x.ID_Grupo,
                        principalTable: "Aluno",
                        principalColumn: "ID_Aluno");
                });

            migrationBuilder.CreateTable(
                name: "Externo",
                columns: table => new
                {
                    ID_Externo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Externo__CF4B12E03BA2BD65", x => x.ID_Externo);
                    table.ForeignKey(
                        name: "FK__Externo__ID_Exte__267ABA7A",
                        column: x => x.ID_Externo,
                        principalTable: "Orientador",
                        principalColumn: "ID_Orientador");
                });

            migrationBuilder.CreateTable(
                name: "Interno",
                columns: table => new
                {
                    ID_Interno = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Interno__EEA2B8265207726E", x => x.ID_Interno);
                    table.ForeignKey(
                        name: "FK__Interno__ID_Inte__29572725",
                        column: x => x.ID_Interno,
                        principalTable: "Orientador",
                        principalColumn: "ID_Orientador");
                });

            migrationBuilder.CreateTable(
                name: "Projeto",
                columns: table => new
                {
                    ID_Projeto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Num_Alunos = table.Column<int>(type: "int", nullable: false),
                    Area_Investigacao = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Centro_Investigacao = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Apresentacao = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false),
                    Objetivos = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    ID_Orientador = table.Column<int>(type: "int", nullable: false),
                    ID_Edicao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Projeto__D3131DB335E3DA52", x => x.ID_Projeto);
                    table.ForeignKey(
                        name: "FK__Projeto__ID_Edic__300424B4",
                        column: x => x.ID_Edicao,
                        principalTable: "Edicao",
                        principalColumn: "ID_Edicao");
                    table.ForeignKey(
                        name: "FK__Projeto__ID_Orie__30F848ED",
                        column: x => x.ID_Orientador,
                        principalTable: "Orientador",
                        principalColumn: "ID_Orientador");
                });

            migrationBuilder.CreateTable(
                name: "Pertence",
                columns: table => new
                {
                    ID_Aluno = table.Column<int>(type: "int", nullable: false),
                    ID_Grupo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pertence__FE5331A80B303854", x => new { x.ID_Aluno, x.ID_Grupo });
                    table.ForeignKey(
                        name: "FK__Pertence__ID_Alu__3F466844",
                        column: x => x.ID_Aluno,
                        principalTable: "Aluno",
                        principalColumn: "ID_Aluno");
                    table.ForeignKey(
                        name: "FK__Pertence__ID_Gru__403A8C7D",
                        column: x => x.ID_Grupo,
                        principalTable: "Grupo",
                        principalColumn: "ID_Grupo");
                });

            migrationBuilder.CreateTable(
                name: "Candidatura",
                columns: table => new
                {
                    ID_Grupo = table.Column<int>(type: "int", nullable: false),
                    ID_projeto = table.Column<int>(type: "int", nullable: false),
                    Seriacao = table.Column<int>(type: "int", nullable: false),
                    Resultado = table.Column<bool>(type: "bit", nullable: true),
                    Ordem_Preferencia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Candidat__994EB61DBEB601F1", x => new { x.ID_Grupo, x.ID_projeto });
                    table.ForeignKey(
                        name: "FK__Candidatu__ID_Gr__37A5467C",
                        column: x => x.ID_Grupo,
                        principalTable: "Grupo",
                        principalColumn: "ID_Grupo");
                    table.ForeignKey(
                        name: "FK__Candidatu__ID_pr__38996AB5",
                        column: x => x.ID_projeto,
                        principalTable: "Projeto",
                        principalColumn: "ID_Projeto");
                });

            migrationBuilder.CreateTable(
                name: "Orienta",
                columns: table => new
                {
                    ID_Orientador = table.Column<int>(type: "int", nullable: false),
                    ID_Projeto = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orienta__EC6A0D1CBD0E5614", x => new { x.ID_Orientador, x.ID_Projeto });
                    table.ForeignKey(
                        name: "FK__Orienta__ID_Orie__3B75D760",
                        column: x => x.ID_Orientador,
                        principalTable: "Orientador",
                        principalColumn: "ID_Orientador");
                    table.ForeignKey(
                        name: "FK__Orienta__ID_Proj__3C69FB99",
                        column: x => x.ID_Projeto,
                        principalTable: "Projeto",
                        principalColumn: "ID_Projeto");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidatura_ID_projeto",
                table: "Candidatura",
                column: "ID_projeto");

            migrationBuilder.CreateIndex(
                name: "IX_Grupo_ID_Edicao",
                table: "Grupo",
                column: "ID_Edicao");

            migrationBuilder.CreateIndex(
                name: "IX_Orienta_ID_Projeto",
                table: "Orienta",
                column: "ID_Projeto");

            migrationBuilder.CreateIndex(
                name: "IX_Pertence_ID_Grupo",
                table: "Pertence",
                column: "ID_Grupo");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_ID_Edicao",
                table: "Projeto",
                column: "ID_Edicao");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_ID_Orientador",
                table: "Projeto",
                column: "ID_Orientador");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunoGrupo");

            migrationBuilder.DropTable(
                name: "Candidatura");

            migrationBuilder.DropTable(
                name: "Externo");

            migrationBuilder.DropTable(
                name: "Interno");

            migrationBuilder.DropTable(
                name: "Orienta");

            migrationBuilder.DropTable(
                name: "Pertence");

            migrationBuilder.DropTable(
                name: "Projeto");

            migrationBuilder.DropTable(
                name: "Grupo");

            migrationBuilder.DropTable(
                name: "Orientador");

            migrationBuilder.DropTable(
                name: "Edicao");

            migrationBuilder.DropTable(
                name: "Aluno");
        }
    }
}
