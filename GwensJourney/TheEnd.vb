Friend Module TheEnd
    Friend Sub Run()
        LegacyPrompts.Clear()
        LegacyPrompts.Add("You made it!")
        SetGameOver()
        LegacyActionItems.Clear()
        LegacyActionItems.Add("Huzzah!", AddressOf MainMenu.Run)
    End Sub
End Module
