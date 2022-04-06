using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Webpage.EFModel
{
    public partial class cosc2650Context : DbContext
    {
        public cosc2650Context(DbContextOptions<cosc2650Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Attachments> Attachments { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<PostCategories> PostCategories { get; set; }
        public virtual DbSet<PostReqResponses> PostReqResponses { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Preference> Preference { get; set; }
        public virtual DbSet<Preferences> Preferences { get; set; }
        public virtual DbSet<Response> Response { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // To protect potentially sensitive information in your connection string, you should move it out of source code.
                // You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148.
                // For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:cosc2650-sp12022.database.windows.net,1433;Initial Catalog=cosc2650;Persist Security Info=False;User ID=cosc2650-sp12022;Password=RIPAntonov!;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Attachments>(entity =>
            {
                entity.HasKey(e => e.Idx)
                    .HasName("attachments_pk");

                entity.ToTable("attachments");

                entity.HasIndex(e => e.Idx, "attachments_idx_uindex")
                    .IsUnique();

                entity.Property(e => e.Idx).HasColumnName("idx");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("content")
                    .IsFixedLength(true);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("description");

                entity.Property(e => e.Filename)
                    .HasMaxLength(1024)
                    .HasColumnName("filename");

                entity.Property(e => e.Mime)
                    .HasMaxLength(64)
                    .HasColumnName("mime");

                entity.Property(e => e.PostIdx).HasColumnName("postIdx");

                entity.HasOne(d => d.PostIdxNavigation)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.PostIdx)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("attachments_posts_idx_fk");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Idx)
                    .HasName("category_pk");

                entity.ToTable("category");

                entity.HasIndex(e => e.Idx, "category_idx_uindex")
                    .IsUnique();

                entity.Property(e => e.Idx).HasColumnName("idx");

                entity.Property(e => e.CategoryType)
                    .HasMaxLength(256)
                    .HasColumnName("categoryType")
                    .HasComment("Category can be for preference, post, offering,...");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.ParentIdx)
                    .HasColumnName("parentIdx")
                    .HasComment("Optional\n");

                entity.HasOne(d => d.ParentIdxNavigation)
                    .WithMany(p => p.InverseParentIdxNavigation)
                    .HasForeignKey(d => d.ParentIdx)
                    .HasConstraintName("category_category_idx_fk");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.Idx)
                    .HasName("location_pk");

                entity.ToTable("location");

                entity.HasIndex(e => e.AreaCode, "location_areaCode_index");

                entity.HasIndex(e => e.Idx, "location_idx_uindex")
                    .IsUnique();

                entity.Property(e => e.Idx).HasColumnName("idx");

                entity.Property(e => e.AreaCode)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("areaCode");

                entity.Property(e => e.Caption)
                    .HasMaxLength(256)
                    .HasColumnName("caption");

                entity.Property(e => e.Country)
                    .HasMaxLength(256)
                    .HasColumnName("country");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.State)
                    .HasMaxLength(256)
                    .HasColumnName("state");

                entity.Property(e => e.WeakLatitude)
                    .HasColumnType("decimal(10, 8)")
                    .HasColumnName("weakLatitude");

                entity.Property(e => e.WeakLongitude)
                    .HasColumnType("decimal(10, 8)")
                    .HasColumnName("weakLongitude");
            });

            modelBuilder.Entity<PostCategories>(entity =>
            {
                entity.HasKey(e => e.Idx)
                    .HasName("postCategories_pk");

                entity.ToTable("postCategories");

                entity.HasIndex(e => e.Idx, "postCategories_idx_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.PostIdx, "postCategories_postIdx_index");

                entity.Property(e => e.Idx).HasColumnName("idx");

                entity.Property(e => e.CategoryIdx).HasColumnName("categoryIdx");

                entity.Property(e => e.PostIdx).HasColumnName("postIdx");

                entity.HasOne(d => d.CategoryIdxNavigation)
                    .WithMany(p => p.PostCategories)
                    .HasForeignKey(d => d.CategoryIdx)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("postCategories_category_idx_fk");

                entity.HasOne(d => d.PostIdxNavigation)
                    .WithMany(p => p.PostCategories)
                    .HasForeignKey(d => d.PostIdx)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("postCategories_posts_idx_fk");
            });

            modelBuilder.Entity<PostReqResponses>(entity =>
            {
                entity.HasKey(e => e.Idx)
                    .HasName("postReqResponses_pk");

                entity.ToTable("postReqResponses");

                entity.HasIndex(e => e.Idx, "postReqResponses_idx_uindex")
                    .IsUnique();

                entity.Property(e => e.Idx).HasColumnName("idx");

                entity.Property(e => e.PostIdx).HasColumnName("postIdx");

                entity.Property(e => e.RespondedOn)
                    .HasColumnName("respondedOn")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.ResponderIdx).HasColumnName("responderIdx");

                entity.Property(e => e.ResponseText)
                    .HasMaxLength(512)
                    .HasColumnName("responseText");

                entity.Property(e => e.ResponseTypeIdx).HasColumnName("responseTypeIdx");

                entity.HasOne(d => d.PostIdxNavigation)
                    .WithMany(p => p.PostReqResponses)
                    .HasForeignKey(d => d.PostIdx)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("postReqResponses_posts_idx_fk");

                entity.HasOne(d => d.ResponderIdxNavigation)
                    .WithMany(p => p.PostReqResponses)
                    .HasForeignKey(d => d.ResponderIdx)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("postReqResponses_users_idx_fk");

                entity.HasOne(d => d.ResponseTypeIdxNavigation)
                    .WithMany(p => p.PostReqResponses)
                    .HasForeignKey(d => d.ResponseTypeIdx)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("postReqResponses_response_idx_fk");
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.HasKey(e => e.Idx)
                    .HasName("posts_pk");

                entity.ToTable("posts");

                entity.HasIndex(e => e.Idx, "posts_idx_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.UserIdx, "posts_userIdx_index");

                entity.Property(e => e.Idx).HasColumnName("idx");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.EndingOn).HasColumnName("endingOn");

                entity.Property(e => e.LocationIdx).HasColumnName("locationIdx");

                entity.Property(e => e.ModifiedOn).HasColumnName("modifiedOn");

                entity.Property(e => e.ParentIdx)
                    .HasColumnName("parentIdx")
                    .HasComment("If this is <> null then the post is a reply.\n");

                entity.Property(e => e.StartingOn).HasColumnName("startingOn");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .HasColumnName("subject");

                entity.Property(e => e.UserIdx).HasColumnName("userIdx");

                entity.HasOne(d => d.LocationIdxNavigation)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.LocationIdx)
                    .HasConstraintName("posts_location_idx_fk");

                entity.HasOne(d => d.ParentIdxNavigation)
                    .WithMany(p => p.InverseParentIdxNavigation)
                    .HasForeignKey(d => d.ParentIdx)
                    .HasConstraintName("posts_posts_idx_fk");

                entity.HasOne(d => d.UserIdxNavigation)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserIdx)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("posts_users_idx_fk");
            });

            modelBuilder.Entity<Preference>(entity =>
            {
                entity.HasKey(e => e.Idx)
                    .HasName("preference_pk");

                entity.ToTable("preference");

                entity.HasIndex(e => e.Idx, "preference_idx_uindex")
                    .IsUnique();

                entity.Property(e => e.Idx).HasColumnName("idx");

                entity.Property(e => e.CategoryIdx)
                    .HasColumnName("categoryIdx")
                    .HasComment("optional, if we want to categorise");

                entity.Property(e => e.Context)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("context")
                    .HasComment("User\nSystem\nAdmin\nBilling...");

                entity.Property(e => e.DataType)
                    .HasMaxLength(256)
                    .HasColumnName("dataType")
                    .HasComment("Helper, if we need to convert data types in code.\n");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.HasOne(d => d.CategoryIdxNavigation)
                    .WithMany(p => p.Preference)
                    .HasForeignKey(d => d.CategoryIdx)
                    .HasConstraintName("preference_category_idx_fk");
            });

            modelBuilder.Entity<Preferences>(entity =>
            {
                entity.HasKey(e => e.Idx)
                    .HasName("preferences_pk");

                entity.ToTable("preferences");

                entity.HasIndex(e => e.Idx, "preferences_prefIdx_uindex")
                    .IsUnique();

                entity.Property(e => e.Idx).HasColumnName("idx");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.PreferenceIdx)
                    .HasColumnName("preferenceIdx")
                    .HasComment("Preference index for type.\n");

                entity.Property(e => e.PreferenceValue)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("preferenceValue")
                    .HasComment("All preferences are stored as nvarchar. If we need a preference that needs to be highly indexable, we can create a separate field.");

                entity.Property(e => e.UpdatedOn).HasColumnName("updatedOn");

                entity.Property(e => e.UserIdx)
                    .HasColumnName("userIdx")
                    .HasComment("Fk to user table\n");

                entity.HasOne(d => d.PreferenceIdxNavigation)
                    .WithMany(p => p.Preferences)
                    .HasForeignKey(d => d.PreferenceIdx)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("preferences_preference_idx_fk");

                entity.HasOne(d => d.UserIdxNavigation)
                    .WithMany(p => p.Preferences)
                    .HasForeignKey(d => d.UserIdx)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("preferences_users_idx_fk");
            });

            modelBuilder.Entity<Response>(entity =>
            {
                entity.HasKey(e => e.Idx)
                    .HasName("response_pk");

                entity.ToTable("response");

                entity.HasIndex(e => e.Idx, "response_idx_uindex")
                    .IsUnique();

                entity.Property(e => e.Idx).HasColumnName("idx");

                entity.Property(e => e.Caption)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("caption");

                entity.Property(e => e.CategoryIdx)
                    .HasColumnName("categoryIdx")
                    .HasComment("Optional category specific response.\n");

                entity.Property(e => e.LogicalResponse)
                    .HasColumnName("logicalResponse")
                    .HasComment("Used for understanding logical response of the entity:\n0 - false : no,never,...\n1 - true : yes,always,...\n2 - unknow: maybe, unknown...\n.\n.\n. ");

                entity.HasOne(d => d.CategoryIdxNavigation)
                    .WithMany(p => p.Response)
                    .HasForeignKey(d => d.CategoryIdx)
                    .HasConstraintName("response_category_idx_fk");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Idx)
                    .HasName("users_pk");

                entity.ToTable("users");

                entity.HasIndex(e => e.Email, "users_email_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Idx, "users_idx_uindex")
                    .IsUnique();

                entity.Property(e => e.Idx).HasColumnName("idx");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(256)
                    .HasColumnName("fullName");

                entity.Property(e => e.LocationIdx).HasColumnName("locationIdx");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(16)
                    .HasColumnName("mobile");

                entity.HasOne(d => d.LocationIdxNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.LocationIdx)
                    .HasConstraintName("users_location_idx_fk");
            });

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.HasKey(e => e.Idx)
                    .HasName("messages_pk");

                entity.ToTable("messages");

                entity.HasIndex(e => e.Idx, "messages_idx_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.SenderIdx, "messages_userIdx_index");

                entity.Property(e => e.Idx).HasColumnName("idx");

                //entity.Property(e => e.Content).HasColumnName("message");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.ModifiedOn).HasColumnName("modifiedOn");

                entity.Property(e => e.ParentIdx)
                    .HasColumnName("parentIdx")
                    .HasComment("If this is <> null then the post is a reply.\n");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .HasColumnName("subject");

               

               // entity.HasOne(d => d.SenderIdxNavigation)
               // .WithMany(p => p.PostReqResponses)
               // .HasForeignKey(d => d.ResponderIdx)
                //.OnDelete(DeleteBehavior.ClientSetNull)
               // .HasConstraintName("postReqResponses_users_idx_fk");



            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
