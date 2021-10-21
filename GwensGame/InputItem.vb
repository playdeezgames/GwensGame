Public Class InputItem
    Public ReadOnly Property Text As String
    Public ReadOnly Property Stuff As Action
    Public Sub New(text As String, stuff As Action)
        Me.Text = text
        Me.Stuff = stuff
    End Sub
End Class
