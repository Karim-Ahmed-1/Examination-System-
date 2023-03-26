using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Examination_system.Models;

public partial class IqexamContext : DbContext
{
    public IqexamContext()
    {
    }

    public IqexamContext(DbContextOptions<IqexamContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Choice> Choices { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<Instructor> Instructors { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentAnswer> StudentAnswers { get; set; }

    public virtual DbSet<StudentGrade> StudentGrades { get; set; }

    public virtual DbSet<Topic> Topics { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=IQExam;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Choice>(entity =>
        {
            entity.Property(e => e.ChoiceId).HasColumnName("Choice_Id");
            entity.Property(e => e.ChoiceBody)
                .HasMaxLength(50)
                .HasColumnName("Choice_Body");
            entity.Property(e => e.QustId).HasColumnName("Qust_Id");

            entity.HasOne(d => d.Qust).WithMany(p => p.Choices)
                .HasForeignKey(d => d.QustId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Choices_Question1");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CrsId);

            entity.ToTable("Course");

            entity.Property(e => e.CrsId).HasColumnName("Crs_Id");
            entity.Property(e => e.CrsName)
                .HasMaxLength(50)
                .HasColumnName("Crs_Name");

            entity.HasMany(d => d.Ins).WithMany(p => p.Crs)
                .UsingEntity<Dictionary<string, object>>(
                    "CourseInstructor",
                    r => r.HasOne<Instructor>().WithMany()
                        .HasForeignKey("InsId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Course_Instructor_Instructor"),
                    l => l.HasOne<Course>().WithMany()
                        .HasForeignKey("CrsId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Course_Instructor_Course"),
                    j =>
                    {
                        j.HasKey("CrsId", "InsId");
                        j.ToTable("Course_Instructor");
                        j.IndexerProperty<int>("CrsId").HasColumnName("Crs_Id");
                        j.IndexerProperty<int>("InsId").HasColumnName("Ins_Id");
                    });

            entity.HasMany(d => d.Stds).WithMany(p => p.Crs)
                .UsingEntity<Dictionary<string, object>>(
                    "CourseStudent",
                    r => r.HasOne<Student>().WithMany()
                        .HasForeignKey("StdId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Course_Student_Student"),
                    l => l.HasOne<Course>().WithMany()
                        .HasForeignKey("CrsId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Course_Student_Course"),
                    j =>
                    {
                        j.HasKey("CrsId", "StdId");
                        j.ToTable("Course_Student");
                        j.IndexerProperty<int>("CrsId").HasColumnName("Crs_Id");
                        j.IndexerProperty<int>("StdId").HasColumnName("Std_Id");
                    });
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DeptId);

            entity.ToTable("Department");

            entity.Property(e => e.DeptId).HasColumnName("Dept_Id");
            entity.Property(e => e.DeptName)
                .HasMaxLength(50)
                .HasColumnName("Dept_Name");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.ToTable("Exam");

            entity.Property(e => e.ExamId).HasColumnName("Exam_Id");
            entity.Property(e => e.CrsId).HasColumnName("Crs_Id");

            entity.HasOne(d => d.Crs).WithMany(p => p.Exams)
                .HasForeignKey(d => d.CrsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Exam_Course");

            entity.HasMany(d => d.Qusts).WithMany(p => p.Exams)
                .UsingEntity<Dictionary<string, object>>(
                    "ExamQuestion",
                    r => r.HasOne<Question>().WithMany()
                        .HasForeignKey("QustId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ExamQuestion_Question"),
                    l => l.HasOne<Exam>().WithMany()
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ExamQuestion_Exam"),
                    j =>
                    {
                        j.HasKey("ExamId", "QustId");
                        j.ToTable("ExamQuestion");
                        j.IndexerProperty<int>("ExamId").HasColumnName("Exam_Id");
                        j.IndexerProperty<int>("QustId").HasColumnName("Qust_Id");
                    });
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasKey(e => e.InsId);

            entity.ToTable("Instructor");

            entity.Property(e => e.InsId).HasColumnName("Ins_Id");
            entity.Property(e => e.DeptId).HasColumnName("Dept_Id");
            entity.Property(e => e.InsName)
                .HasMaxLength(50)
                .HasColumnName("Ins_Name");
            entity.Property(e => e.InsPass)
                .HasMaxLength(50)
                .HasColumnName("Ins_Pass");

            entity.HasOne(d => d.Dept).WithMany(p => p.Instructors)
                .HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Instructor_Department");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuesId);

            entity.ToTable("Question");

            entity.Property(e => e.QuesId).HasColumnName("Ques_Id");
            entity.Property(e => e.CrsId).HasColumnName("Crs_Id");
            entity.Property(e => e.QustBody)
                .HasMaxLength(300)
                .HasColumnName("Qust_Body");
            entity.Property(e => e.QustMarks).HasColumnName("Qust_Marks");
            entity.Property(e => e.QustModelAnsId).HasColumnName("Qust_ModelAns_Id");
            entity.Property(e => e.QustType).HasColumnName("Qust_Type");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StdId);

            entity.ToTable("Student");

            entity.Property(e => e.StdId).HasColumnName("Std_Id");
            entity.Property(e => e.DeptId).HasColumnName("Dept_Id");
            entity.Property(e => e.StdAge).HasColumnName("Std_Age");
            entity.Property(e => e.StdName)
                .HasMaxLength(50)
                .HasColumnName("Std_Name");
            entity.Property(e => e.StdPass)
                .HasMaxLength(50)
                .HasColumnName("Std_Pass");

            entity.HasOne(d => d.Dept).WithMany(p => p.Students)
                .HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Department");
        });

        modelBuilder.Entity<StudentAnswer>(entity =>
        {
            entity.HasKey(e => new { e.AnsId, e.ExamId, e.StdId });

            entity.ToTable("Student_Answer");

            entity.Property(e => e.AnsId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Ans_Id");
            entity.Property(e => e.ExamId).HasColumnName("Exam_Id");
            entity.Property(e => e.StdId).HasColumnName("Std_Id");
            entity.Property(e => e.AnsBody).HasColumnName("Ans_body");
            entity.Property(e => e.QustId).HasColumnName("Qust_Id");

            entity.HasOne(d => d.Exam).WithMany(p => p.StudentAnswers)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Answer_Exam");

            entity.HasOne(d => d.Qust).WithMany(p => p.StudentAnswers)
                .HasForeignKey(d => d.QustId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Answer_Question");

            entity.HasOne(d => d.Std).WithMany(p => p.StudentAnswers)
                .HasForeignKey(d => d.StdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Answer_Student");
        });

        modelBuilder.Entity<StudentGrade>(entity =>
        {
            entity.HasKey(e => e.GradeId);

            entity.ToTable("Student_Grades");

            entity.Property(e => e.GradeId).HasColumnName("Grade_Id");
            entity.Property(e => e.ExamId).HasColumnName("Exam_Id");
            entity.Property(e => e.StdId).HasColumnName("Std_Id");

            entity.HasOne(d => d.Exam).WithMany(p => p.StudentGrades)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Grades_Exam");

            entity.HasOne(d => d.Std).WithMany(p => p.StudentGrades)
                .HasForeignKey(d => d.StdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Grades_Student");
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.ToTable("Topic");

            entity.Property(e => e.TopicId).HasColumnName("Topic_Id");
            entity.Property(e => e.CrsId).HasColumnName("Crs_Id");
            entity.Property(e => e.TopicName)
                .HasMaxLength(50)
                .HasColumnName("Topic_Name");

            entity.HasOne(d => d.Crs).WithMany(p => p.Topics)
                .HasForeignKey(d => d.CrsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Topic_Course");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
