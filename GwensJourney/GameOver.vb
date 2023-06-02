Friend Module GameOver
    Friend Sub Run()
        Prompts.Clear()
        Prompts.Add($"[red]Yer dead![/]")
        ActionItems.Clear()
        ActionItems.Add("That sucks!", AddressOf MainMenu.Run)
    End Sub
End Module
