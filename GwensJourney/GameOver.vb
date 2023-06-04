Friend Module GameOver
    Friend Sub Run()
        LegacyPrompts.Clear()
        LegacyPrompts.Add($"[red]Yer dead![/]")
        LegacyActionItems.Clear()
        LegacyActionItems.Add("That sucks!", AddressOf MainMenu.Run)
    End Sub
End Module
