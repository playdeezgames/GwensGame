Public Class Prompt
    ReadOnly DisplayText As String
    ReadOnly Color As ConsoleColor
    Sub New(text As String, color As ConsoleColor)
        DisplayText = text
        Me.Color = color
    End Sub
End Class
