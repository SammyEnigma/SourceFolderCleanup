This is a small utility I'm working on to free up disk space on my Surface machine which has a relatively small 256GB drive. My main repo folder has large `package/`, `bin/` and `obj/` folders that accumulate a lot of stuff that doesn't need to be there all the time. There might something out there that already does this nicely, but as usual, this is just something I like working on.

![img](https://adamosoftware.blob.core.windows.net:443/images/source-folder-cleanup.png)

This makes use of my [WinForms.Library](https://github.com/adamosoftware/WinForms.Library) project to handle the form data binding with a `ControlBinder<T>` object, incidentally. I'm also using my [JsonSettings.Library](https://github.com/adamosoftware/JsonSettings) project to handle settings persistence in json. My `Settings` model is [here](https://github.com/adamosoftware/SourceFolderCleanup/blob/master/SourceFolderCleanup/Models/Settings.cs).

```csharp
private void frmMain_Load(object sender, System.EventArgs e)
{
	var monthValues = new int[] { 3, 6, 9, 12 }.Select(i => new ListItem<int>(i, i.ToString()));

	_settings = SettingsBase.Load<Settings>();
	_binder = new ControlBinder<Settings>();            

	_binder.Add(tbSourcePath, model => model.SourcePath);
	_binder.Add(chkDelete, model => model.Delete);
	_binder.Add(chkDeleteBinObj, model => model.DeleteBinAndObj);
	_binder.Add(chkDeletePackages, model => model.DeletePackages);
	
	_binder.AddItems(cbDeleteMonths,
		(model) => model.DeleteMonthsOld = cbDeleteMonths.GetValue<int>(),
		(model) => cbDeleteMonths.SetValue(model.DeleteMonthsOld), monthValues);

	_binder.Add(chkArchive, model => model.Archive);
	_binder.Add(tbArchivePath, model => model.ArchivePath);
	_binder.AddItems(cbArchiveMonths,
		(model) => model.ArchiveMonthsOld = cbArchiveMonths.GetValue<int>(),
		(model) => cbArchiveMonths.SetValue(model.ArchiveMonthsOld), monthValues);

	_binder.Document = _settings;
	chkArchive_CheckedChanged(null, new EventArgs());
	chkDelete_CheckedChanged(null, new EventArgs());
}
```
