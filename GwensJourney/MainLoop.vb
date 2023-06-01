Friend Module MainLoop
    Friend Sub Run()
        While HasActions()
            Do
                Show()
            Loop Until SelectAction()
        End While
    End Sub
End Module
