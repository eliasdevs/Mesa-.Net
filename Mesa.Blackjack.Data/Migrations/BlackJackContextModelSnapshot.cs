﻿// <auto-generated />
using System;
using Mesa.Blackjack.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Mesa.BlackJack.Data.Migrations
{
    [DbContext(typeof(BlackJackContext))]
    partial class BlackJackContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Mesa.Blackjack.Blackjack", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ContadorMazo")
                        .HasColumnType("int");

                    b.Property<string>("IdRequest")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdRequest")
                        .IsUnique();

                    b.ToTable("Blackjacks");
                });

            modelBuilder.Entity("Mesa.Blackjack.GameRequestBackJack", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AcceptedPlayerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlayerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TipoJuego")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GameRequest", (string)null);
                });

            modelBuilder.Entity("Mesa_SV.DeckOfCards", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(14)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("DeckOfCard", (string)null);
                });

            modelBuilder.Entity("Mesa_SV.Mensaje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(14)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<DateTime>("FechaEnvio")
                        .HasMaxLength(14)
                        .HasColumnType("datetime2");

                    b.Property<string>("Remitente")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("idReceptor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Mensajes", (string)null);
                });

            modelBuilder.Entity("Mesa.Blackjack.Blackjack", b =>
                {
                    b.HasOne("Mesa.Blackjack.GameRequestBackJack", null)
                        .WithOne("backjack")
                        .HasForeignKey("Mesa.Blackjack.Blackjack", "IdRequest")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsMany("Mesa.BlackJack.ManoJugador", "ManoJugadores", b1 =>
                        {
                            b1.Property<string>("BlackjackId")
                                .HasColumnType("nvarchar(50)");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("IdJugador")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<int>("estado")
                                .HasColumnType("int");

                            b1.HasKey("BlackjackId", "Id");

                            b1.ToTable("ManoJugador");

                            b1.WithOwner()
                                .HasForeignKey("BlackjackId");

                            b1.OwnsMany("Mesa_SV.VoDeJuegos.Card", "Mano", b2 =>
                                {
                                    b2.Property<string>("ManoJugadorBlackjackId")
                                        .HasColumnType("nvarchar(50)");

                                    b2.Property<int>("ManoJugadorId")
                                        .HasColumnType("int");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b2.Property<int>("Id"));

                                    b2.Property<int>("OriginalValue")
                                        .HasColumnType("int");

                                    b2.Property<string>("Representation")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<int>("SubValue")
                                        .HasColumnType("int");

                                    b2.Property<int>("TypeOfCardId")
                                        .HasColumnType("int");

                                    b2.HasKey("ManoJugadorBlackjackId", "ManoJugadorId", "Id");

                                    b2.ToTable("BlackJack_Active_Hand", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("ManoJugadorBlackjackId", "ManoJugadorId");
                                });

                            b1.Navigation("Mano");
                        });

                    b.OwnsMany("Mesa_SV.VoDeJuegos.Card", "Mazo", b1 =>
                        {
                            b1.Property<string>("BlackjackId")
                                .HasColumnType("nvarchar(50)");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<int>("OriginalValue")
                                .HasColumnType("int");

                            b1.Property<string>("Representation")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("SubValue")
                                .HasColumnType("int");

                            b1.Property<int>("TypeOfCardId")
                                .HasColumnType("int");

                            b1.HasKey("BlackjackId", "Id");

                            b1.ToTable("BlackJack_Mazo", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("BlackjackId");
                        });

                    b.OwnsMany("Mesa_SV.VoDeJuegos.HistoryBlackJackVo", "History", b1 =>
                        {
                            b1.Property<string>("Id")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("BlackjackId")
                                .IsRequired()
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("IdJugador")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Logger")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("nvarchar(500)");

                            b1.Property<int>("contadorMazo")
                                .HasMaxLength(10)
                                .HasColumnType("int");

                            b1.HasKey("Id");

                            b1.HasIndex("BlackjackId");

                            b1.ToTable("BlackJack_History", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("BlackjackId");

                            b1.OwnsMany("Mesa_SV.VoDeJuegos.Card", "PlayerHand", b2 =>
                                {
                                    b2.Property<string>("HistoryBlackJackVoId")
                                        .HasColumnType("nvarchar(50)");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b2.Property<int>("Id"));

                                    b2.Property<int>("OriginalValue")
                                        .HasColumnType("int");

                                    b2.Property<string>("Representation")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<int>("SubValue")
                                        .HasColumnType("int");

                                    b2.Property<int>("TypeOfCardId")
                                        .HasColumnType("int");

                                    b2.HasKey("HistoryBlackJackVoId", "Id");

                                    b2.ToTable("BlackJack_History_Player", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("HistoryBlackJackVoId");
                                });

                            b1.Navigation("PlayerHand");
                        });

                    b.Navigation("History");

                    b.Navigation("ManoJugadores");

                    b.Navigation("Mazo");
                });

            modelBuilder.Entity("Mesa.Blackjack.GameRequestBackJack", b =>
                {
                    b.OwnsMany("Mesa_SV.InfoJugador", "PlayerInfo", b1 =>
                        {
                            b1.Property<string>("GameRequestBackJackId")
                                .HasColumnType("nvarchar(50)");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("IdContextWS")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("IdUser")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("GameRequestBackJackId", "Id");

                            b1.ToTable("InfoJugador");

                            b1.WithOwner()
                                .HasForeignKey("GameRequestBackJackId");
                        });

                    b.Navigation("PlayerInfo");
                });

            modelBuilder.Entity("Mesa_SV.DeckOfCards", b =>
                {
                    b.OwnsMany("Mesa_SV.VoDeJuegos.Card", "Cards", b1 =>
                        {
                            b1.Property<int>("DeckOfCardsId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<int>("OriginalValue")
                                .HasMaxLength(50)
                                .HasColumnType("int");

                            b1.Property<string>("Representation")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<int>("SubValue")
                                .HasMaxLength(50)
                                .HasColumnType("int");

                            b1.Property<int>("TypeOfCardId")
                                .HasMaxLength(50)
                                .HasColumnType("int");

                            b1.HasKey("DeckOfCardsId", "Id");

                            b1.ToTable("DeckOfCard_Cards", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("DeckOfCardsId");
                        });

                    b.Navigation("Cards");
                });

            modelBuilder.Entity("Mesa.Blackjack.GameRequestBackJack", b =>
                {
                    b.Navigation("backjack");
                });
#pragma warning restore 612, 618
        }
    }
}
