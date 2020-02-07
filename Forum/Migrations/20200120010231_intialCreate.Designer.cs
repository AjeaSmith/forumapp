﻿// <auto-generated />
using System;
using Forum.ForumData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Forum.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200120010231_intialCreate")]
    partial class intialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1");

            modelBuilder.Entity("Forum.ForumData.Models.Forum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Forums");
                });

            modelBuilder.Entity("Forum.ForumData.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ForumId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ForumId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Forum.ForumData.Models.PostReply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PostId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("PostReplies");
                });

            modelBuilder.Entity("Forum.ForumData.Models.Post", b =>
                {
                    b.HasOne("Forum.ForumData.Models.Forum", "Forum")
                        .WithMany("Posts")
                        .HasForeignKey("ForumId");
                });

            modelBuilder.Entity("Forum.ForumData.Models.PostReply", b =>
                {
                    b.HasOne("Forum.ForumData.Models.Post", "Post")
                        .WithMany("PostReplies")
                        .HasForeignKey("PostId");
                });
#pragma warning restore 612, 618
        }
    }
}
