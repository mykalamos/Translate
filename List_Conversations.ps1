cls
Get-ChildItem -Path C:\Users\Riky\Documents\Code\Translate\Conversations -Recurse -Include '*.txt' | 
% {new-object PsObject -Property @{ Name = $_.Name; Parent = if($_.Directory.Name -eq 'Conversations') {''} else {$_.Directory.Name }; }}

