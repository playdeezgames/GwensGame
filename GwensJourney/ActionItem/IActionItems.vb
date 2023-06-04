Public Interface IActionItems
    Sub Clear()
    Sub Add(text As String, action As Action)
    Function Choose() As Boolean
    Function HasAny() As Boolean
End Interface
