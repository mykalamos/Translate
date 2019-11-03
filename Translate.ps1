using namespace System.Text.RegularExpressions;
using namespace System.IO;
using namespace System.Web;

$phrases = `
    'hello', `
    'goodbye', `
    'thank you', `
    'thank you very much', `
    'Do you speak English', `
    'Sorry I do not speak Japanese' `
;

$targetEncoding = 'fr';
$options = [RegexOptions]::Compiled -bor [RegexOptions]::CultureInvariant -bor [RegexOptions]::IgnoreCase;
$regex = New-Object Regex('"(?<h>[^"]+)"', $options)
foreach($phrase in $phrases){
    $urlEncodedPhrase = [HttpUtility]::UrlEncode($phrase);
    $outputPath = "C:\Users\Riky\Documents\Translate\$phrase-$targetEncoding.json";
    Invoke-WebRequest `
        -Uri "https://translate.googleapis.com/translate_a/single?client=gtx&sl=EN&tl=$targetEncoding&dt=t&q=$urlEncodedPhrase" `
        -OutFile $outputPath `
        -PassThru `
        -Method Get `
        ;
    [System.Threading.Thread]::Sleep(1000);
    $a = [File]::ReadAllLines($outputPath);
    
    $matches = $regex.Matches($a[0]);
    $list = New-Object Collections.Generic.List[String]
    foreach($match in $matches){
        foreach($group in $match.Groups){
            if($group.Name -eq '0'){
                continue;
            }
            foreach($capture in $group.Captures){
                $list.Add( $capture.Value);
                if($list.Count -eq 2){
                    break;
                }
            }
        }
    }        

    for($i=0; $i -lt $list.Count; $i++){
        $item = $list[$i];
        $len = $item.Length;
        $lang = 'en'
        if($i -eq 0){
            $lang = $targetEncoding
        }
        $urlEncodedItem = [HttpUtility]::UrlEncode($item);
        Invoke-WebRequest `
            -Uri "https://translate.google.com/translate_tts?ie=UTF-8&client=tw-ob&q=$urlEncodedItem&tl=$lang&total=1&idx=0&textlen=$len" `
            -OutFile "C:\Users\Riky\Documents\Translate\$phrase-$lang.mp3" `
            -PassThru `
            -Method Get `
        ;
        [System.Threading.Thread]::Sleep(1000);
    }
}


# $options = [RegexOptions]::Compiled -bor [RegexOptions]::CultureInvariant -bor [RegexOptions]::IgnoreCase;
# $regex = New-Object Regex('"(?<h>[^"]+)"', $options)

# $matches = $regex.Matches("[[[`"こんにちは`",`"hello`",null,null,1]");

# foreach($match in $matches){
#  foreach($group in $match.Groups){
#     if($group.Name -eq '0'){
#         continue;
#     }
#     foreach($capture in $group.Captures){
#         Write-Host $capture.Value
#     }
#  }
# }



# $a = [File]::ReadAllLines("C:\Users\Riky\Documents\Translate\hello-ja.json")

# Write-Host $a[0]