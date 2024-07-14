﻿// <auto-generated />
using System;
using Library_Management_EF_Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Library_Management_EF_Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240714144821_ReturnBookChanged")]
    partial class ReturnBookChanged
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Library_Management_EF_Core.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Library_Management_EF_Core.Models.AuthorBook", b =>
                {
                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("AuthorId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("AuthorsBooks");
                });

            modelBuilder.Entity("Library_Management_EF_Core.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BorrowerId")
                        .HasColumnType("int");

                    b.Property<string>("Desc")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("LoanId")
                        .HasColumnType("int");

                    b.Property<int>("PublishedYear")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(50)");

                    b.HasKey("Id");

                    b.HasIndex("BorrowerId");

                    b.HasIndex("LoanId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Library_Management_EF_Core.Models.Borrower", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(40)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(40)");

                    b.HasKey("Id");

                    b.ToTable("Borrowers");
                });

            modelBuilder.Entity("Library_Management_EF_Core.Models.Loan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BorrowerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LoanDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("MustReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BorrowerId");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("Library_Management_EF_Core.Models.LoanItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("LoanId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("LoanId");

                    b.ToTable("LoanItems");
                });

            modelBuilder.Entity("Library_Management_EF_Core.Models.AuthorBook", b =>
                {
                    b.HasOne("Library_Management_EF_Core.Models.Author", "Author")
                        .WithMany("AuthorBooks")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library_Management_EF_Core.Models.Book", "Book")
                        .WithMany("AuthorBooks")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("Library_Management_EF_Core.Models.Book", b =>
                {
                    b.HasOne("Library_Management_EF_Core.Models.Borrower", "Borrower")
                        .WithMany("Books")
                        .HasForeignKey("BorrowerId");

                    b.HasOne("Library_Management_EF_Core.Models.Loan", null)
                        .WithMany("Books")
                        .HasForeignKey("LoanId");

                    b.Navigation("Borrower");
                });

            modelBuilder.Entity("Library_Management_EF_Core.Models.Loan", b =>
                {
                    b.HasOne("Library_Management_EF_Core.Models.Borrower", "Borrower")
                        .WithMany("Loans")
                        .HasForeignKey("BorrowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Borrower");
                });

            modelBuilder.Entity("Library_Management_EF_Core.Models.LoanItem", b =>
                {
                    b.HasOne("Library_Management_EF_Core.Models.Book", "Book")
                        .WithMany("LoanItems")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Library_Management_EF_Core.Models.Loan", "Loan")
                        .WithMany("LoanItems")
                        .HasForeignKey("LoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Loan");
                });

            modelBuilder.Entity("Library_Management_EF_Core.Models.Author", b =>
                {
                    b.Navigation("AuthorBooks");
                });

            modelBuilder.Entity("Library_Management_EF_Core.Models.Book", b =>
                {
                    b.Navigation("AuthorBooks");

                    b.Navigation("LoanItems");
                });

            modelBuilder.Entity("Library_Management_EF_Core.Models.Borrower", b =>
                {
                    b.Navigation("Books");

                    b.Navigation("Loans");
                });

            modelBuilder.Entity("Library_Management_EF_Core.Models.Loan", b =>
                {
                    b.Navigation("Books");

                    b.Navigation("LoanItems");
                });
#pragma warning restore 612, 618
        }
    }
}
