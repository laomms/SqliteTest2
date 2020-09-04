<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.textBox3 = New System.Windows.Forms.TextBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.textBox2 = New System.Windows.Forms.TextBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.textBox1 = New System.Windows.Forms.TextBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.button5 = New System.Windows.Forms.Button()
        Me.button4 = New System.Windows.Forms.Button()
        Me.button3 = New System.Windows.Forms.Button()
        Me.button2 = New System.Windows.Forms.Button()
        Me.listView1 = New System.Windows.Forms.ListView()
        Me.button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'textBox3
        '
        Me.textBox3.Location = New System.Drawing.Point(411, 59)
        Me.textBox3.Name = "textBox3"
        Me.textBox3.Size = New System.Drawing.Size(124, 20)
        Me.textBox3.TabIndex = 23
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(367, 62)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(40, 13)
        Me.label3.TabIndex = 22
        Me.label3.Text = "CODE:"
        '
        'textBox2
        '
        Me.textBox2.Location = New System.Drawing.Point(228, 59)
        Me.textBox2.Name = "textBox2"
        Me.textBox2.Size = New System.Drawing.Size(128, 20)
        Me.textBox2.TabIndex = 21
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(184, 62)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(42, 13)
        Me.label2.TabIndex = 20
        Me.label2.Text = "EMAIL:"
        '
        'textBox1
        '
        Me.textBox1.Location = New System.Drawing.Point(53, 59)
        Me.textBox1.Name = "textBox1"
        Me.textBox1.Size = New System.Drawing.Size(119, 20)
        Me.textBox1.TabIndex = 19
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(9, 62)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(38, 13)
        Me.label1.TabIndex = 18
        Me.label1.Text = "Name:"
        '
        'button5
        '
        Me.button5.Location = New System.Drawing.Point(112, 12)
        Me.button5.Name = "button5"
        Me.button5.Size = New System.Drawing.Size(102, 25)
        Me.button5.TabIndex = 17
        Me.button5.Text = "插入数据库"
        Me.button5.UseVisualStyleBackColor = True
        '
        'button4
        '
        Me.button4.Location = New System.Drawing.Point(436, 12)
        Me.button4.Name = "button4"
        Me.button4.Size = New System.Drawing.Size(102, 25)
        Me.button4.TabIndex = 16
        Me.button4.Text = "删除数据库"
        Me.button4.UseVisualStyleBackColor = True
        '
        'button3
        '
        Me.button3.Location = New System.Drawing.Point(328, 12)
        Me.button3.Name = "button3"
        Me.button3.Size = New System.Drawing.Size(102, 25)
        Me.button3.TabIndex = 15
        Me.button3.Text = "修改数据库"
        Me.button3.UseVisualStyleBackColor = True
        '
        'button2
        '
        Me.button2.Location = New System.Drawing.Point(220, 12)
        Me.button2.Name = "button2"
        Me.button2.Size = New System.Drawing.Size(102, 25)
        Me.button2.TabIndex = 14
        Me.button2.Text = "读取数据库"
        Me.button2.UseVisualStyleBackColor = True
        '
        'listView1
        '
        Me.listView1.HideSelection = False
        Me.listView1.Location = New System.Drawing.Point(4, 99)
        Me.listView1.Name = "listView1"
        Me.listView1.Size = New System.Drawing.Size(534, 187)
        Me.listView1.TabIndex = 13
        Me.listView1.UseCompatibleStateImageBehavior = False
        '
        'button1
        '
        Me.button1.Location = New System.Drawing.Point(4, 12)
        Me.button1.Name = "button1"
        Me.button1.Size = New System.Drawing.Size(102, 25)
        Me.button1.TabIndex = 12
        Me.button1.Text = "创建数据库"
        Me.button1.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(545, 292)
        Me.Controls.Add(Me.textBox3)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.textBox2)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.textBox1)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.button5)
        Me.Controls.Add(Me.button4)
        Me.Controls.Add(Me.button3)
        Me.Controls.Add(Me.button2)
        Me.Controls.Add(Me.listView1)
        Me.Controls.Add(Me.button1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents textBox3 As TextBox
    Private WithEvents label3 As Label
    Private WithEvents textBox2 As TextBox
    Private WithEvents label2 As Label
    Private WithEvents textBox1 As TextBox
    Private WithEvents label1 As Label
    Private WithEvents button5 As Button
    Private WithEvents button4 As Button
    Private WithEvents button3 As Button
    Private WithEvents button2 As Button
    Private WithEvents listView1 As ListView
    Private WithEvents button1 As Button
End Class
