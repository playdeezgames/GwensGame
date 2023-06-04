Public Interface IPrompts
    Function Clear() As IPrompts
    Function Add(prompt As String) As IPrompts
    Function Show() As IPrompts
End Interface
