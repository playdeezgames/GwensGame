Public Interface IApplicationState
    Sub Handle(tokens As IEnumerable(Of String))
    Sub Run()
End Interface
