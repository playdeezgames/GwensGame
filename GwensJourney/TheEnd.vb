Friend Module TheEnd
    Friend Sub Run()
        Prompts.Clear()
        Prompts.Add("You made it!")
        SetGameOver()
        ActionItems.Clear()
        ActionItems.Add("Huzzah!", AddressOf MainMenu.Run)
    End Sub
End Module
