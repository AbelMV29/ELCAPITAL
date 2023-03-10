using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELCAPITAL.Migrations
{
    /// <inheritdoc />
    public partial class DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoDocumento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroDocumento = table.Column<int>(type: "int", nullable: false),
                    Ingresos = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DineroEnCuenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipoCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreEmpresa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "ElCapitalFondos",
                columns: table => new
                {
                    IdBancoUnico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FondoMonetario = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElCapitalFondos", x => x.IdBancoUnico);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    CualProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EsCrediticio = table.Column<bool>(type: "bit", nullable: true),
                    EsPrendario = table.Column<bool>(type: "bit", nullable: true),
                    DineroPrestamo = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FechaLimite = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.IdProducto);
                    table.ForeignKey(
                        name: "FK_Producto_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restriccions",
                columns: table => new
                {
                    IdRestriccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Motivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restriccions", x => x.IdRestriccion);
                    table.ForeignKey(
                        name: "FK_Restriccions_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Solicitudes",
                columns: table => new
                {
                    IdSolicitud = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoSolicitud = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitudes", x => x.IdSolicitud);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transferencias",
                columns: table => new
                {
                    IdTransferencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MontoTransferido = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaTrans = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CVUDestino = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transferencias", x => x.IdTransferencia);
                    table.ForeignKey(
                        name: "FK_transferencias_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TarjetaDeCreditos",
                columns: table => new
                {
                    IdTarjetaDeCredito = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoTarjeta = table.Column<int>(type: "int", nullable: false),
                    ClaveTarjeta = table.Column<int>(type: "int", nullable: false),
                    CVU = table.Column<int>(type: "int", nullable: false),
                    DineroEnTarjeta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarjetaDeCreditos", x => x.IdTarjetaDeCredito);
                    table.ForeignKey(
                        name: "FK_TarjetaDeCreditos_Producto_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormularioRaizs",
                columns: table => new
                {
                    IdFormularioRaiz = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaAprobacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdSolicitud = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormularioRaizs", x => x.IdFormularioRaiz);
                    table.ForeignKey(
                        name: "FK_FormularioRaizs_Solicitudes_IdSolicitud",
                        column: x => x.IdSolicitud,
                        principalTable: "Solicitudes",
                        principalColumn: "IdSolicitud",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormularioRechazos",
                columns: table => new
                {
                    IdFormularioRechazo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Motivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaRechazo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdSolicitud = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormularioRechazos", x => x.IdFormularioRechazo);
                    table.ForeignKey(
                        name: "FK_FormularioRechazos_Solicitudes_IdSolicitud",
                        column: x => x.IdSolicitud,
                        principalTable: "Solicitudes",
                        principalColumn: "IdSolicitud",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormularioRaizs_IdSolicitud",
                table: "FormularioRaizs",
                column: "IdSolicitud",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormularioRechazos_IdSolicitud",
                table: "FormularioRechazos",
                column: "IdSolicitud",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Producto_IdCliente",
                table: "Producto",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Restriccions_IdCliente",
                table: "Restriccions",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_IdCliente",
                table: "Solicitudes",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_TarjetaDeCreditos_IdProducto",
                table: "TarjetaDeCreditos",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_transferencias_IdCliente",
                table: "transferencias",
                column: "IdCliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElCapitalFondos");

            migrationBuilder.DropTable(
                name: "FormularioRaizs");

            migrationBuilder.DropTable(
                name: "FormularioRechazos");

            migrationBuilder.DropTable(
                name: "Restriccions");

            migrationBuilder.DropTable(
                name: "TarjetaDeCreditos");

            migrationBuilder.DropTable(
                name: "transferencias");

            migrationBuilder.DropTable(
                name: "Solicitudes");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
