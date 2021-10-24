Public Interface IActionItem
    ReadOnly Property DisplayText As String
    ReadOnly Property Color As ConsoleColor
    Sub Perform()
End Interface
