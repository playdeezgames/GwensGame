Public Class ActionItem
    Implements IActionItem

    Private performAction As Action

    Public Sub New(text As String, action As Action, color As ConsoleColor)
        DisplayText = text
        performAction = action
        Me.Color = color
    End Sub

    Public ReadOnly Property DisplayText As String Implements IActionItem.DisplayText

    Public ReadOnly Property Color As ConsoleColor Implements IActionItem.Color

    Public Sub Perform() Implements IActionItem.Perform
        performAction()
    End Sub
End Class
