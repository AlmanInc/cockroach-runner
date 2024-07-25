mergeInto(LibraryManager.library,
{		
	GetUserName: function()
	{
		var userName = '';
		
		try
		{
			let tg = window.Telegram.WebApp;
			
			userName = tg.initDataUnsafe.user.first_name;
			userName = userName.toString();
		
			// Object-method-args
			myGameInstance.SendMessage('JSJob', 'LoadUserName', userName);
		}
		catch (err)
		{
			window.alert(err.toString());
			userName = "Failed User";
			userName = userName.toString();
			myGameInstance.SendMessage('JSJob', 'LoadUserName', userName);
		}
	},
	
	GetUserId: function()
	{
		var userId = '';
		
		try
		{
			let tg = window.Telegram.WebApp;
			
			userId = tg.initDataUnsafe.user.id;		
			userId = userId.toString();
		
			myGameInstance.SendMessage('JSJob', 'LoadUserId', userId);
		}
		catch (err)
		{
			window.alert(err.toString());
			userId = 15;
			userId = userId.toString();
			myGameInstance.SendMessage('JSJob', 'LoadUserId', userId);
		}		
	},
	
	GetUserRef: function()
	{
		var refId = '';
		
		try
		{				
			var ref_id = 'tgWebAppStartParam';
		
			if (ref_id=(new RegExp('[?&]'+encodeURIComponent(ref_id)+'=([^&]*)')).exec(location.search))
				refId = decodeURIComponent(ref_id[1]); 
		
			refId = refId.toString();
			myGameInstance.SendMessage('JSJob', 'LoadUserRefId', refId);
		}
		catch (err)
		{
			window.alert(err.toString());
			refId = refId.toString();
			myGameInstance.SendMessage('JSJob', 'LoadUserRefId', refId);
		}
    },
	
	ShowMessage: function(str)
	{
		window.alert(UTF8ToString(str));
    },
	
	CopyReferalLink: function(str)
	{
		document.getElementsByTagName('html')[0].focus();
		navigator.clipboard.writeText(UTF8ToString(str));
    }
});