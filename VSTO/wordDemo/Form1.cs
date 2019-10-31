using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xceed.Words.NET;
using System.IO;


namespace WordDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //创建文档
            using (DocX document = DocX.Create(@"HelloWorld.docx"))
            {
                //插入段落
              Paragraph p = document.InsertParagraph();

                //段落加入文本，定义格式
                p.Append("Hello World!")     //内容
                .Font(new Xceed.Words.NET.Font("宋体")) //字体格式
                .FontSize(20)   //字体大小
                .Color(Color.Blue)  //字体颜色
                .Bold()
                .Alignment = Alignment.center;  //字体对其方式

                p = document.InsertParagraph();  //创建段落
                p.Append("CHINA")
                .Font(new Xceed.Words.NET.Font("微软雅黑"))
                .FontSize(18)
                .Color(Color.Red);

                //插入表格4x4，使用addtable后再inserttable
                var table1 = document.AddTable(9, 4);
                table1.Design = TableDesign.TableGrid;

                for (int i = 0; i < 9; i++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        table1.Rows[i].Cells[k].Paragraphs.First().InsertText(string.Format("第{0}行，第{1}列", i, k));
                    }
                }
                //合并单元格
                table1.MergeCellsInColumn(0, 0, 1);
                //////水平合并（合并第1行的第1、2个单元格）
                //table1.ApplyHorizontalMerge(0, 0, 1);
                //table1.Rows[0].Cells[0].Paragraphs[0].ChildObjects.Add(table.Rows[0].Cells[1].Paragraphs[0].ChildObjects[0]);
 
                ////垂直合并（合并第1列的第3、4个单元格）
                //table.ApplyVerticalMerge(0,2, 3);            
                //table.Rows[2].Cells[0].Paragraphs[0].ChildObjects.Add(table.Rows[3].Cells[0].Paragraphs[0].ChildObjects[0]);
 

                document.InsertTable(table1);

                //追加行
                document.Tables[0].InsertRow().Cells[1].Paragraphs.First().InsertText("新添11加的行");
                //插入图片
                p = document.InsertParagraph();
                Xceed.Words.NET.Image image = document.AddImage(@"1.jpg");
                Picture pic= image.CreatePicture();
                p.InsertPicture(pic);
                p.Alignment = Alignment.right;
                //保存文档
                document.Save();
            
            }
            MessageBox.Show("Done!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string templateFile = @"template.docx";
            string dstFile = @"result.docx";
            File.Copy(templateFile, dstFile, true);
            using (DocX document = DocX.Load(dstFile))
            {
                //替换文字
                document.ReplaceText("#日期#", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                //找到表格并插入
                for (int i = 0; i < 5; i++)
                {
                    document.Tables[0].InsertRow().Cells[1].Paragraphs.First().InsertText("新添加的行");
                }
                document.Tables[0].Rows[0].Remove();
                //保存
                document.Save();
            }
            MessageBox.Show("Done!");
        }
    }
}
