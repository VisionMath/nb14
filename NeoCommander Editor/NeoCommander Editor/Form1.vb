Imports System
Imports System.IO
Imports Ionic.Zip
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Text
Imports System.Runtime.InteropServices

Public Class Form1

    Declare Auto Function LWMemoryDecode Lib "koeilw.dll" _
      Alias "_LW_MemoryDecode@24" (ByVal source() As Byte, ByVal slen As Int32, _
                                        ByRef result As IntPtr, ByRef olen As Int32, ByVal msg As Int32, ByVal hWnd As IntPtr) As Boolean

    Declare Auto Function LWMemoryEncode Lib "koeilw.dll" _
       Alias "_LW_MemoryEncode@24" (ByVal source() As Byte, ByVal slen As Int32, _
                                         ByRef result As IntPtr, ByRef olen As Int32, ByVal msg As Int32, ByVal hWnd As IntPtr) As Boolean

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=False)> _
    Private Shared Function SendMessage(ByVal hwnd As IntPtr, ByVal wMsg As Int32, ByVal wParam As Int32, ByVal lParam As Int32) As Int32
    End Function

    Const LVM_FIRST As Integer = &H1000
    Const LVM_SETICONSPACING As Integer = LVM_FIRST + 53

    Public kordic = "가각간갇갈갉갊감갑값갓갔강갖갗같갚갛개객갠갤갬갭갯갰갱갸갹갼걀걋걍걔걘걜거걱건걷걸걺검겁것겄겅겆겉겊겋게겐겔겜겝겟겠겡겨격겪견겯결겸겹겻겼경곁계곈곌곕곗고곡곤곧골곪곬곯곰곱곳공곶과곽관괄괆괌괍괏광괘괜괠괩괬괭괴괵괸괼굄굅굇굉교굔굘굡굣구국군굳굴굵굶굻굼굽굿궁궂궈궉권궐궜궝궤궷귀귁귄귈귐귑귓규균귤그극근귿글긁금급긋긍긔기긱긴긷길긺김깁깃깅깆깊까깍깎깐깔깖깜깝깟깠깡깥깨깩깬깰깸깹깻깼깽꺄꺅꺌꺼꺽꺾껀껄껌껍껏껐껑께껙껜껨껫껭껴껸껼꼇꼈꼍꼐꼬꼭꼰꼲꼴꼼꼽꼿꽁꽂꽃꽈꽉꽐꽜꽝꽤꽥꽹꾀꾄꾈꾐꾑꾕꾜꾸꾹꾼꿀꿇꿈꿉꿋꿍꿎꿔꿜꿨꿩꿰꿱꿴꿸뀀뀁뀄뀌뀐뀔뀜뀝뀨끄끅끈끊끌끎끓끔끕끗끙끝끼끽낀낄낌낍낏낑나낙낚난낟날낡낢남납낫났낭낮낯낱낳내낵낸낼냄냅냇냈냉냐냑냔냘냠냥너넉넋넌널넒넓넘넙넛넜넝넣네넥넨넬넴넵넷넸넹녀녁년녈념녑녔녕녘녜녠노녹논놀놂놈놉놋농높놓놔놘놜놨뇌뇐뇔뇜뇝뇟뇨뇩뇬뇰뇹뇻뇽누눅눈눋눌눔눕눗눙눠눴눼뉘뉜뉠뉨뉩뉴뉵뉼늄늅늉느늑는늘늙늚늠늡늣능늦늪늬늰늴니닉닌닐닒님닙닛닝닢다닥닦단닫달닭닮닯닳담답닷닸당닺닻닿대댁댄댈댐댑댓댔댕댜더덕덖던덛덜덞덟덤덥덧덩덪덫덮데덱덴델뎀뎁뎃뎄뎅뎌뎐뎔뎠뎡뎨뎬도독돈돋돌돎돐돔돕돗동돛돝돠돤돨돼됐되된될됨됩됫됴두둑둔둘둠둡둣둥둬뒀뒈뒝뒤뒨뒬뒵뒷뒹듀듄듈듐듕드득든듣들듦듬듭듯등듸디딕딘딛딜딤딥딧딨딩딪따딱딴딸텭땀땁땃땄땅땋때땍땐땔땜땝땟땠땡떠떡떤떨떪떫떰떱떳떴떵떻떼떽뗀뗄뗌뗍뗏뗐뗑뗘뗬또똑똔똘똥똬똴뙈뙤뙨뚜뚝뚠뚤뚫뚬뚱뛔뛰뛴뛸뜀뜁뜅뜨뜩뜬뜯뜰뜸뜹뜻띄띈띌띔띕띠띤띨띰띱띳띵라락란랄람랍랏랐랑랒랖랗퇏래랙랜랠램랩랫랬랭랴략랸럇량러럭런럴럼럽럿렀렁렇레렉렌렐렘렙렛렝려력련렬렴렵렷렸령례롄롑롓로록론롤롬롭롯롱롸롼뢍뢨뢰뢴뢸룀룁룃룅료룐룔룝룟룡루룩룬룰룸룹룻룽뤄뤘뤠뤼뤽륀륄륌륏륑류륙륜률륨륩툩륫륭르륵른를름릅릇릉릊릍릎리릭린릴림립릿링마막만많맏말맑맒맘맙맛망맞맡맣매맥맨맬맴맵맷맸맹맺먀먁먈먕머먹먼멀멂멈멉멋멍멎멓메멕멘멜멤멥멧멨멩며멱면멸몃몄명몇몌모목몫몬몰몲몸몹못몽뫄뫈뫘뫙뫼묀묄묍묏묑묘묜묠묩묫무묵묶문묻물묽묾뭄뭅뭇뭉뭍뭏뭐뭔뭘뭡뭣뭬뮈뮌뮐뮤뮨뮬뮴뮷므믄믈믐믓미믹민믿밀밂밈밉밋밌밍및밑바박밖밗반받발밝밞밟밤밥밧방밭배백밴밸뱀뱁뱃뱄뱅뱉뱌뱍뱐뱝버벅번벋벌벎범법벗벙벚베벡벤벧벨벰벱벳벴벵벼벽변별볍볏볐병볕볘볜보복볶본볼봄봅봇봉봐봔봤봬뵀뵈뵉뵌뵐뵘뵙뵤뵨부북분붇불붉붊붐붑붓붕붙붚붜붤붰붸뷔뷕뷘뷜뷩뷰뷴뷸븀븃븅브븍븐블븜븝븟비빅빈빌빎빔빕빗빙빚빛빠빡빤빨빪빰빱빳빴빵빻빼빽뺀뺄뺌뺍뺏뺐뺑뺘뺙뺨뻐뻑뻔뻗뻘뻠뻣뻤뻥뻬뼁뼈뼉뼘뼙뼛뼜뼝뽀뽁뽄뽈뽐뽑뽕뾔뾰뿅뿌뿍뿐뿔뿜뿟뿡쀼쁑쁘쁜쁠쁨쁩삐삑삔삘삠삡삣삥사삭삯산삳살삵삶삼삽삿샀상샅새색샌샐샘샙샛샜생샤샥샨샬샴샵샷샹섀섄섈섐섕서석섞섟선섣설섦섧섬섭섯섰성섶세섹센셀셈셉셋셌셍셔셕션셜셤셥셧셨셩셰셴셸솅소속솎손솔솖솜솝솟송솥솨솩솬솰솽쇄쇈쇌쇔쇗쇘쇠쇤쇨쇰쇱쇳쇼쇽숀숄숌숍숏숑수숙순숟술숨숩숫숭숯숱숲숴쉈쉐쉑쉔쉘쉠쉥쉬쉭쉰쉴쉼쉽쉿슁슈슉슐슘슛슝스슥슨슬슭슴습슷승시식신싣실싫심십싯싱싶싸싹싻싼쌀쌈쌉쌌쌍쌓쌔쌕쌘쌜쌤쌥쌨쌩썅써썩썬썰썲썸썹썼썽쎄쎈쎌쏀쏘쏙쏜쏟쏠쏢쏨쏩쏭쏴쏵쏸쐈쐐쐤쐬쐰쐴쐼쐽쑈쑤쑥쑨쑬쑴쑵쑹쒀쒔쒜쒸쒼쓩쓰쓱쓴쓸쓺쓿씀씁씌씐씔씜씨씩씬씰씸씹씻씽아악안앉않알앍앎앓암압앗았앙앝앞애액앤앨앰앱앳앴앵야약얀얄얇얌얍얏양얕얗얘얜얠얩어억언얹얻얼얽얾엄업없엇었엉엊엌엎에엑엔엘엠엡엣엥여역엮연열엶엷염엽엾엿였영옅옆옇예옌옐옘옙옛옜오옥온올옭옮옰옳옴옵옷옹옻와왁완왈왐왑왓왔왕왜왝왠왬왯왱외왹왼욀욈욉욋욍요욕욘욜욤욥욧용우욱운울욹욺움웁웃웅워웍원월웜웝웠웡웨웩웬웰웸웹웽위윅윈윌윔윕윗윙유육윤율윰윱윳융윷으윽은을읊음읍읏응읒읓읔읕읖읗의읜읠읨읫이익인일읽읾잃임입잇있잉잊잎자작잔잖잗잘잚잠잡잣잤장잦재잭잰잴잼잽잿쟀쟁쟈쟉쟌쟎쟐쟘쟝쟤쟨쟬저적전절젊점접젓정젖제젝젠젤젬젭젯젱져젼졀졈졉졌졍졔조족존졸졺좀좁좃종좆좇좋좌좍좔좝좟좡좨좼좽죄죈죌죔죕죗죙죠죡죤죵주죽준줄줅줆줌줍줏중줘줬줴쥐쥑쥔쥘쥠쥡쥣쥬쥰쥴쥼즈즉즌즐즘즙즛증지직진짇질짊짐집짓징짖짙짚짜짝짠짢짤짧짬짭짯짰짱째짹짼쨀쨈쨉쨋쨌쨍쨔쨘쨩쩌쩍쩐쩔쩜쩝쩟쩠쩡쩨쩽쪄쪘쪼쪽쫀쫄쫌쫍쫏쫑쫓쫘쫙쫠쫬쫴쬈쬐쬔쬘쬠쬡쭁쭈쭉쭌쭐쭘쭙쭝쭤쭸쭹쮜쮸쯔쯤쯧쯩찌찍찐찔찜찝찡찢찧차착찬찮찰참찹찻찼창찾채책챈챌챔챕챗챘챙챠챤챦챨챰챵처척천철첨첩첫첬청체첵첸첼쳄쳅쳇쳉쳐쳔쳤쳬쳰촁초촉촌촐촘촙촛총촤촨촬촹최쵠쵤쵬쵭쵯쵱쵸춈추축춘출춤춥춧충춰췄췌췐취췬췰췸췹췻췽츄츈츌츔츙츠측츤츨츰츱츳층치칙친칟칠칡침칩칫칭카칵칸칼캄캅캇캉캐캑캔캘캠캡캣캤캥캬캭컁커컥컨컫컬컴컵컷컸컹케켁켄켈켐켑켓켕켜켠켤켬켭켯켰켱켸코콕콘콜콤콥콧콩콰콱콴콸쾀쾅쾌쾡쾨쾰쿄쿠쿡쿤쿨쿰쿱쿳쿵쿼퀀퀄퀑퀘퀭퀴퀵퀸퀼큄큅큇큉큐큔큘큠크큭큰클큼큽킁키킥킨킬킴킵킷킹타탁탄탈탉탐탑탓탔탕태택탠탤탬탭탯탰탱탸턍터턱턴털턺텀텁텃텄텅테텍텐텔템텝텟텡텨텬텼톄톈토톡톤톨톰톱톳통톺톼퇀퇘퇴퇸툇툉툐투툭툰툴툼툽툿퉁퉈퉜퉤튀튁튄튈튐튑튕튜튠튤튬튱트특튼튿틀틂틈틉틋틔틘틜틤틥티틱틴틸팀팁팃팅파팍팎판팔팖팜팝팟팠팡팥패팩팬팰팸팹팻팼팽퍄퍅퍼퍽펀펄펌펍펏펐펑페펙펜펠펨펩펫펭펴편펼폄폅폈평폐폘폡폣포폭폰폴폼폽폿퐁퐈퐝푀푄표푠푤푭푯푸푹푼푿풀풂품풉풋풍풔풩퓌퓐퓔퓜퓟퓨퓬퓰퓸퓻퓽프픈플픔픕픗피픽핀필핌핍핏핑하학한할핥함합핫항해핵핸핼햄햅햇했행햐향허헉헌헐헒험헙헛헝헤헥헨헬헴헵헷헹혀혁현혈혐협혓혔형혜혠혤혭호혹혼홀홅홈홉홋홍홑화확환활홧황홰홱홴횃횅회획횐횔횝횟횡효횬횰횹횻후훅훈훌훑훔훗훙훠훤훨훰훵훼훽휀휄휑휘휙휜휠휨휩휫휭휴휵휸휼흄흅흉흐흑흔흖흗흘흙흠흡흣흥흩희흰흴흼흽힁히힉힌힘힐힙힛"
    Public Jisdic = "家各姦Γ葛駅丑感甲ΔΕΘ姜ΛΨγηξ開客ψБЁЖЙЦ粳Ю醵榎奄鴛⊥凹旺欧居∟建⊿傑襖檢怯∪л鴬旭飴粟闇偈荻億牡卸伽萎佳茨格珂犬禾結兼稼苛蝦經磯界蚊俄芽蛾高谷困拐骨械灰芥蟹劾崖工侃科郭官括碍浬馨垣光掛蛎鈎劃嚇核傀掴赫顎鰍潟恰宏撹渇滑褐鎌九國君厩屈噛ё鴨栢郁茅宮萱粥憾權闕澗潅机缶貴莞閑舘巌翫伎圭均橘棋極根汽軌飢今岌犠兢祇基蟻緊掬吉吃桔砧杵黍汲灸亨僑兇娃薗峡蕎尭桐禽芹菌衿襟吟区狗玖躯駈駒寓串釧屑沓靴窪謂隈粂栗Г薫袈啓径慧憩畦罫頚鯨拳軒鹸諺糊袴股菰鈷檎瑚醐鯉喉坑垢庚拘稿糠肯肱腔郊鉱砿閤劫濠甑坤墾梱痕紺唆嵯瑳裟坐挫栽犀阪堺榊肴埼碕咋搾鮭笹擦皐鯖捌鮫皿晒撒燦珊蚕酸餐仔屍枝祉Д誌爾痔汐軸宍那諾悉暖偲捺蕊斜南納紗杓娘灼惹趣愁耐拾洲臭蒐酋什夙冷竣准旬楯曙渚薯藷鋤娼宵抄梢樵沼硝粧菖蕉裳訟醤鉦鞘剰壌嬢擾女蒸年醸念錠嘱寧埴禰拭努穐蝕伸榛疹紳薪農塵腎В訊靭諏悩睡粋椙菅摺嫋畝醒脆脊蹟摂耨煎嫩煽吶穿羨腺舛詮賎践銑閃漸膳糎紐衄噌岨措曽疏租遡叢匝惣掻槽漕能痩糟鎗霜臓尼溺捉昵俗其葵揃詑唾多蔭妥團楕達騨胎腿苔淡答鯛醍當鷹瀧啄大宅濁茸蛸但竪坦箪綻蛋徳弛畜窒宙註酎渥樗苧凋芦喋帖暢牒腸蝶諜捗椎塚栂漬鍔坪嬬紬爪鶴道獨敦汀突禎訂蹄滴笛洞鏑哲轍迭纏甜貼顛澱菟砥凍套搭兜淫屯棟痘糖謄豆鐙憧撞萄瞳峠鴇匿涜栃凸鳶苫寅酉瀞得奈乍凪灘馴楠弐等匂賑廿尿韮祢猫捻撚乃埜膿覗蚤播杷琶婆芭俳排肺培楳狽賠這蝿萩粕舶曝漠莫駁函箱硲箸櫨畠鉢溌筏鳩噺塙蛤汎販釆頒匪緋誹枇琵柊稗疋髭肘畢桧謬瓢票秒鋲蒜蛭鰭斌瀕瓶埠冨鯵芙葡葺蕗吋弗鮒噴墳扮紛雰丙蔽頁僻碧瞥箆遍娩螺落乱剌覽拉戊虻浪菩倣峯庖來泡烹縫胞蓬剖坊帽鵜略某肪量膨貿鉾頬撲釦殆昧哩槙膜柾鱒亦俣繭麿箕砺力恋烈廉獵湊蓑澪禮稔鵡椋鷺綠論牝棉麺摸籠妄儲杢餅瀬尤紋匁爺愉佑料悠揖柚湧龍累涌猷郵傭曜溶熔窯蓉沃淀莱酪欄濫梨璃流六倫律痢葎掠琉隆寮勒瞭稜凛諒姐陵厘淋燐理瑠隣塁臨立嶺怜馬幕萬苓憐末漣簾聯廊榔望篭聾蝋賣脈歪枠亙亘鰐詫盟蕨椀碗亢丐个丱丶丼丿乖乘亅亳亶从仍仄仞价估佛佝佇佶侏覓面滅侘佻明侑袂模木佯侖沒儘俔俟俎夢俘俛俚俐俤俥倔倪倥倅苗伜倡倩倬武墨俾文俯物們倆假偕偖偬傚傴僊傳僂僖僞僥僮價僵儖儕儔儚儡儺儷儻儿兒美兌民兔密竸兮冂囘冏冑冓冕冖朴冤冦反冢發冩絢冪冫决冱方冲配白冰况姥冽凅凉几欝凩凭凾刋刎刧繁刪罰刮範法刳刹剪剴剳剿剽劔劒剱劈劑辨辧壁變別劬劭劼炳劵勁勍保福勞本勦飭勵勸奉勹匆匍匐匏渦匕匚唄匯匱匳匸部北分區不卆卅丗卍凖棚卩卮夘卻厂厖厠厦厥厮厰厶叟叮叨叭叺吁吽呀听吼吝呎悲咏貧咎呟呱呷呰氷咒臼呻咀哇咢咥咬哄哈Е哂咾咼哘哦唏唔哽閏哮哭哺哢唹啀啣啌售啜啅啗唸唳啝喙鰻喀咯喊喟啻啾喞啼喩喇嗚嗅嗟嗜嗤嗔嘔嗷嘖嗾嗽嘛嗹噎嘶嘸噫嘯嚆嚀嚊嚠嚔嚏嚥囂嚼囁社朔囃傘囀殺囈按三渋囓囗上圀塞色囿圄圉圈圓圖生嗇圦圷坎圻址坏坩垈坿垉垠垳西石垤垪善垰設埃埒繊燮埖埣成堋世塲堡塰塒堽墅墹墫壞墸墮壅壑壗壙壘壥壜壤壯壻所速壼孫蟀壽夂夊夐宋夛梦夥夬夭夲刷奚奘奧奬奩衰奸妁妝侫妣妲姨妍姙娥娟娑娉水淑順娚術婀婬婉崇娵婢婪媼嫂嫗嫖嫺嫻嬋嬖淬嬲嫐荏嬪嬶嬾孃孀孑孕孛孩孰孳斈孺蝨它宸湿寃勝匙植新寉實寐心十寢寞寥寫寳尅尓尢尨屁屆雙屓孱屬屮乢屶岔岫岻岶岼岷峅峇峭嶌崋崕崗嵜崟崛崑崢崚崙崘嵎嵬嵳嵶嶇嶄嶢嶝嶬嶮嶽嶐嶼巉巍巓巒巛帚帙幃幀幎幗幔幟幢幇麼广庠廁廂廈廐廏廣廝廚廛廡廩氏廱廳廰廴廸廾弉亞惡安彝彜斡弑弖弭巖壓弸彁央彈彎愛額弯彑彗彡彳彷桜野鰯液徃徂彿洩徊襄很徑徇從徙徘漁憶言徠徨蘖忻忤嚴業忸忱忝悳忿怡恠恚怙円怐怩怱怛怫餘易怦然熱怏恁炎燁恍恃恂英頴恫恙叡悁悍悃悚悄悖五玉穩兀悗悧悋悸惓悴忰擁悽瓦惆完曰悵慍愆惶王倭惷鞍愀惴惺外愃愡惻愾愨愧慊要慾愼愬愴愽慂用宇勗云鬱慳慷慘慙蔚雄慚慫原月慴慯慥慱慝慓慵憙憖憇憊位憑憮懊懈懃憺懋有育尹聿罹懍懣融懶懺懴隠乙懾音邑戉應戍戌戔戛戞戡毅戮戳扁扞二益人日扣扛扠任入扨扼剩找抒自作殘抓抖拔抃暫雜抔拗奨抻在拏拿拆擔拈拜拌爭拊拂拇抛挌拮拱挧挂拯著的全節拵店接捐政捍済搜捏掎掀捶掣掏掉掟掵捫捩揀揆揣操族存卒搆搦搶搏種摧摶攪左撕撓撥撩撈據擅擇罪撻擘擂舉擡抬擯攬擴擺主竹俊茁攤攫攴攷收中畋敕敍敘敝敲斂斃斛斫斷旃旆旁旒即旙櫛怎汁无證地織唇旡質旱斟輯杲澄昊昃昴昜晄晁晞晤晨晟晢晰暃暄暘暾暼暸曖曚昿曦曩曵曷朏朖朦朿杁朸朷杆杞杠杙杣杤杰枌枋枦枡枅枷柬枳柩枸柤柞柝柢柮柆柧栞框桍栲桎梳桙档桷桿梏梭梔梛梃檮梹梵桾椁棊椢棡椌棍棔棧棕椄且着贊棗察參椥棹棠昌棯債策椨椪椚椣椡棆楹楜楫楾楮椹楴處拓川鐵添捷椽椰晴体楡楞榁楪榲榮槐槁槓榾槝榻樮楚囑村榠榕榴槨總槿槲撮槧崔樞槭樔槫樒櫁樣樓追築春出橄橲樶忠橸橦萃樢取檐檍檣檗蘗檻櫃櫂檸檳櫟櫚側齔櫪欅櫺欒層治則親欖七欟侵蟄欸稱欷盜欹歃歉歐歙歔歛歟歡歹歿殄殃殍殕殞殤殪殫殯殱殳殼毆毟毬毫毳毯麾氓气氛氤汞汕汢汪沂沍沚汾沽泗泝沱沺泯泙泪洟洸洙洵洳洒洌浣涓快浤浙涎涕淹渕淇淦涸淆淌淒淅淺淙淕淪湮渮湲湟渣湫渫湶湍渟湃渺湎溂渝溘滉溷滓溯滄溲滔溏滂滾滲漱他託炭奪滯探塔漲滌宕太澤潺潸澁澀潛濳潭澂澎澳澣澡濆濕濔濘濛瀋濺瀑瀁瀏濾瀛瀚潴瀘瀟瀰瀲炒炯土烱噸炬炸炮烟通烋烝焙熈退煢煖熕熨投熹熾燉燔燎燵燼燹燿爛爨爰爻爼爿牀牋牘牴牾犁犇犒特緯犧犲狆闖狎狒狢倏猊猴猯猩猥獏獗獻獺珈玳波珎玻判八珀珥珮璢琥珸琲敗琺瑕琿瑟璞瓏瓔彭珱愎瓠瓣瓧瓩瓮瓲瓰瓱瓷甅甎甍甦甼畄畍畊畉偏畆貶畚畩平廢畤畧畫浦暴畭畸疆畴疊疉疂疔疚疝表疥疣疳痃疽疸疼疱痍痊品痒痣豊痞痾痿痼瘁痰痺痲痳瘋瘉瘧瘠瘡瘢癇癈癆癜皮癘癡必癢逼癨癩河學寒割癪咸合癧港海覈癬癰癲癶皀皃行皈向嘘皋憲歇皙驗皚皰皴盖盞盪蘯盻眇眄眤眛革懸血嫌協眷眸形惠睚睨睛乎惑混忽睥睿睾睹洪瞋化拡煥活瞑黄瞠瞶瞿瞼瞽會獲瞻矇矍矗橫孝矜矮砌砒後礦勳砠礪碎硴薨碆喧硼碚碌卉磆磋碾碼揮磅磬磧磚磽磴礇休礑礙恤礬礫凶祕釛欣祓祿屹禊欽吸禝興禧熙齋禳禺秕秬秡稍稘稙詰稟禀"

    'Dim code(1) As Byte
    'Dim pos As UInt32 = &H4684
    'Dim grpstream As FileStream = File.Open("res_grp.bin", FileMode.Open)

    Public header64() As Byte = {&H42, &H4D, &H36, &H40, &H0, &H0, &H0, &H0, &H0, &H0, &H36, &H0, &H0, &H0, &H28, &H0, &H0, &H0, &H40, &H0, &H0, &H0, &HC0, &HFF, &HFF, &HFF, &H1, &H0, &H20, &H0, &H0, &H0, &H0, &H0, &H0, &H40, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}
    Public header128() As Byte = {&H42, &H4D, &H36, &H0, &H1, &H0, &H0, &H0, &H0, &H0, &H36, &H0, &H0, &H0, &H28, &H0, &H0, &H0, &H80, &H0, &H0, &H0, &H80, &HFF, &HFF, &HFF, &H1, &H0, &H20, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H1, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}
    Public header192() As Byte = {&H42, &H4D, &H36, &H0, &H2, &H0, &H0, &H0, &H0, &H0, &H36, &H0, &H0, &H0, &H28, &H0, &H0, &H0, &H0, &H1, &H0, &H0, &H80, &HFF, &HFF, &HFF, &H1, &H0, &H20, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H2, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}
    Public header256() As Byte = {&H42, &H4D, &H36, &H0, &H4, &H0, &H0, &H0, &H0, &H0, &H36, &H0, &H0, &H0, &H28, &H0, &H0, &H0, &H0, &H1, &H0, &H0, &H0, &HFF, &HFF, &HFF, &H1, &H0, &H20, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H4, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}
    Public header512() As Byte = {&H42, &H4D, &H36, &H0, &H20, &H0, &H0, &H0, &H0, &H0, &H36, &H0, &H0, &H0, &H28, &H0, &H0, &H0, &H0, &H2, &H0, &H0, &H0, &HFC, &HFF, &HFF, &H1, &H0, &H20, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H20, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}
    'Public gtheader() As Byte = {&H47, &H54, &H31, &H47, &H30, &H35, &H30, &H30, &H2C, &H40, &H0, &H0, &H20, &H0, &H0, &H0, &H1, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H4, &H0, &H0, &H0, &H10, &H1, &H66, &H0, &H0, &H10, &H21, &H0}

    Public gtg256() As Byte = {&H47, &H54, &H31, &H47, &H30, &H36, &H30, &H30, &H38, &H0, &H1, &H0, &H20, &H0, &H0, &H0, &H1, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H4, &H0, &H0, &H0, &H10, &H8, &H88, &H0, &H11, &H11, &H1, &H10, &HC, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H1}
    Public gtg512() As Byte = {&H47, &H54, &H31, &H47, &H30, &H36, &H30, &H30, &H38, &H0, &H8, &H0, &H20, &H0, &H0, &H0, &H1, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H4, &H0, &H0, &H0, &H10, &H8, &HA9, &H0, &H11, &H11, &H1, &H10, &HC, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H1}
    Public gtg192() As Byte = {&H47, &H54, &H31, &H47, &H30, &H36, &H30, &H30, &H38, &H80, &H0, &H0, &H20, &H0, &H0, &H0, &H1, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H4, &H0, &H0, &H0, &H10, &H8, &H78, &H0, &H11, &H11, &H1, &H10, &HC, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H1}
    Public gtg128() As Byte = {&H47, &H54, &H31, &H47, &H30, &H36, &H30, &H30, &H38, &H40, &H0, &H0, &H20, &H0, &H0, &H0, &H1, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H4, &H0, &H0, &H0, &H10, &H8, &H77, &H0, &H11, &H11, &H1, &H10, &HC, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H1}
    Public gtg64() As Byte = {&H47, &H54, &H31, &H47, &H30, &H36, &H30, &H30, &H38, &H10, &H0, &H0, &H20, &H0, &H0, &H0, &H1, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H4, &H0, &H0, &H0, &H10, &H8, &H66, &H0, &H11, &H11, &H1, &H10, &HC, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H1}

    Public neogen() As Byte = File.ReadAllBytes("neogen.dat")

    '가각간갇갈갉갊감갑값갓갔강갖갗같갚갛개객갠갤갬갭갯갰갱갸갹갼걀걋걍걔걘걜거걱건걷걸걺검겁것겄겅겆겉겊겋게겐겔겜겝겟겠겡겨격겪견겯결겸겹겻겼경곁계곈곌곕곗고곡곤곧골곪곬곯곰곱곳공곶과곽관괄괆괌괍괏광괘괜괠괩괬괭괴괵괸괼굄굅굇굉교굔굘굡굣구국군굳굴굵굶굻굼굽굿궁궂궈궉권궐궜궝궤궷귀귁귄귈귐귑귓규균귤그극근귿글긁금급긋긍긔기긱긴긷길긺김깁깃깅깆깊까깍깎깐깔깖깜깝깟깠깡깥깨깩깬깰깸깹깻깼깽꺄꺅꺌꺼꺽꺾껀껄껌껍껏껐껑께껙껜껨껫껭껴껸껼꼇꼈꼍꼐꼬꼭꼰꼲꼴꼼꼽꼿꽁꽂꽃꽈꽉꽐꽜꽝꽤꽥꽹꾀꾄꾈꾐꾑꾕꾜꾸꾹꾼꿀꿇꿈꿉꿋꿍꿎꿔꿜꿨꿩꿰꿱꿴꿸뀀뀁뀄뀌뀐뀔뀜뀝뀨끄끅끈끊끌끎끓끔끕끗끙끝끼끽낀낄낌낍낏낑나낙낚난낟날낡낢남납낫났낭낮낯낱낳내낵낸낼냄냅냇냈냉냐냑냔냘냠냥너넉넋넌널넒넓넘넙넛넜넝넣네넥넨넬넴넵넷넸넹녀녁년녈념녑녔녕녘녜녠노녹논놀놂놈놉놋농높놓놔놘놜놨뇌뇐뇔뇜뇝뇟뇨뇩뇬뇰뇹뇻뇽누눅눈눋눌눔눕눗눙눠눴눼뉘뉜뉠뉨뉩뉴뉵뉼늄늅늉느늑는늘늙늚늠늡늣능늦늪늬늰늴니닉닌닐닒님닙닛닝닢다닥닦단닫달닭닮닯닳담답닷닸당닺닻닿대댁댄댈댐댑댓댔댕댜더덕덖던덛덜덞덟덤덥덧덩덪덫덮데덱덴델뎀뎁뎃뎄뎅뎌뎐뎔뎠뎡뎨뎬도독돈돋돌돎돐돔돕돗동돛돝돠돤돨돼됐되된될됨됩됫됴두둑둔둘둠둡둣둥둬뒀뒈뒝뒤뒨뒬뒵뒷뒹듀듄듈듐듕드득든듣들듦듬듭듯등듸디딕딘딛딜딤딥딧딨딩딪따딱딴딸텭땀땁땃땄땅땋때땍땐땔땜땝땟땠땡떠떡떤떨떪떫떰떱떳떴떵떻떼떽뗀뗄뗌뗍뗏뗐뗑뗘뗬또똑똔똘똥똬똴뙈뙤뙨뚜뚝뚠뚤뚫뚬뚱뛔뛰뛴뛸뜀뜁뜅뜨뜩뜬뜯뜰뜸뜹뜻띄띈띌띔띕띠띤띨띰띱띳띵라락란랄람랍랏랐랑랒랖랗퇏래랙랜랠램랩랫랬랭랴략랸럇량러럭런럴럼럽럿렀렁렇레렉렌렐렘렙렛렝려력련렬렴렵렷렸령례롄롑롓로록론롤롬롭롯롱롸롼뢍뢨뢰뢴뢸룀룁룃룅료룐룔룝룟룡루룩룬룰룸룹룻룽뤄뤘뤠뤼뤽륀륄륌륏륑류륙륜률륨륩툩륫륭르륵른를름릅릇릉릊릍릎리릭린릴림립릿링마막만많맏말맑맒맘맙맛망맞맡맣매맥맨맬맴맵맷맸맹맺먀먁먈먕머먹먼멀멂멈멉멋멍멎멓메멕멘멜멤멥멧멨멩며멱면멸몃몄명몇몌모목몫몬몰몲몸몹못몽뫄뫈뫘뫙뫼묀묄묍묏묑묘묜묠묩묫무묵묶문묻물묽묾뭄뭅뭇뭉뭍뭏뭐뭔뭘뭡뭣뭬뮈뮌뮐뮤뮨뮬뮴뮷므믄믈믐믓미믹민믿밀밂밈밉밋밌밍및밑바박밖밗반받발밝밞밟밤밥밧방밭배백밴밸뱀뱁뱃뱄뱅뱉뱌뱍뱐뱝버벅번벋벌벎범법벗벙벚베벡벤벧벨벰벱벳벴벵벼벽변별볍볏볐병볕볘볜보복볶본볼봄봅봇봉봐봔봤봬뵀뵈뵉뵌뵐뵘뵙뵤뵨부북분붇불붉붊붐붑붓붕붙붚붜붤붰붸뷔뷕뷘뷜뷩뷰뷴뷸븀븃븅브븍븐블븜븝븟비빅빈빌빎빔빕빗빙빚빛빠빡빤빨빪빰빱빳빴빵빻빼빽뺀뺄뺌뺍뺏뺐뺑뺘뺙뺨뻐뻑뻔뻗뻘뻠뻣뻤뻥뻬뼁뼈뼉뼘뼙뼛뼜뼝뽀뽁뽄뽈뽐뽑뽕뾔뾰뿅뿌뿍뿐뿔뿜뿟뿡쀼쁑쁘쁜쁠쁨쁩삐삑삔삘삠삡삣삥사삭삯산삳살삵삶삼삽삿샀상샅새색샌샐샘샙샛샜생샤샥샨샬샴샵샷샹섀섄섈섐섕서석섞섟선섣설섦섧섬섭섯섰성섶세섹센셀셈셉셋셌셍셔셕션셜셤셥셧셨셩셰셴셸솅소속솎손솔솖솜솝솟송솥솨솩솬솰솽쇄쇈쇌쇔쇗쇘쇠쇤쇨쇰쇱쇳쇼쇽숀숄숌숍숏숑수숙순숟술숨숩숫숭숯숱숲숴쉈쉐쉑쉔쉘쉠쉥쉬쉭쉰쉴쉼쉽쉿슁슈슉슐슘슛슝스슥슨슬슭슴습슷승시식신싣실싫심십싯싱싶싸싹싻싼쌀쌈쌉쌌쌍쌓쌔쌕쌘쌜쌤쌥쌨쌩썅써썩썬썰썲썸썹썼썽쎄쎈쎌쏀쏘쏙쏜쏟쏠쏢쏨쏩쏭쏴쏵쏸쐈쐐쐤쐬쐰쐴쐼쐽쑈쑤쑥쑨쑬쑴쑵쑹쒀쒔쒜쒸쒼쓩쓰쓱쓴쓸쓺쓿씀씁씌씐씔씜씨씩씬씰씸씹씻씽아악안앉않알앍앎앓암압앗았앙앝앞애액앤앨앰앱앳앴앵야약얀얄얇얌얍얏양얕얗얘얜얠얩어억언얹얻얼얽얾엄업없엇었엉엊엌엎에엑엔엘엠엡엣엥여역엮연열엶엷염엽엾엿였영옅옆옇예옌옐옘옙옛옜오옥온올옭옮옰옳옴옵옷옹옻와왁완왈왐왑왓왔왕왜왝왠왬왯왱외왹왼욀욈욉욋욍요욕욘욜욤욥욧용우욱운울욹욺움웁웃웅워웍원월웜웝웠웡웨웩웬웰웸웹웽위윅윈윌윔윕윗윙유육윤율윰윱윳융윷으윽은을읊음읍읏응읒읓읔읕읖읗의읜읠읨읫이익인일읽읾잃임입잇있잉잊잎자작잔잖잗잘잚잠잡잣잤장잦재잭잰잴잼잽잿쟀쟁쟈쟉쟌쟎쟐쟘쟝쟤쟨쟬저적전절젊점접젓정젖제젝젠젤젬젭젯젱져젼졀졈졉졌졍졔조족존졸졺좀좁좃종좆좇좋좌좍좔좝좟좡좨좼좽죄죈죌죔죕죗죙죠죡죤죵주죽준줄줅줆줌줍줏중줘줬줴쥐쥑쥔쥘쥠쥡쥣쥬쥰쥴쥼즈즉즌즐즘즙즛증지직진짇질짊짐집짓징짖짙짚짜짝짠짢짤짧짬짭짯짰짱째짹짼쨀쨈쨉쨋쨌쨍쨔쨘쨩쩌쩍쩐쩔쩜쩝쩟쩠쩡쩨쩽쪄쪘쪼쪽쫀쫄쫌쫍쫏쫑쫓쫘쫙쫠쫬쫴쬈쬐쬔쬘쬠쬡쭁쭈쭉쭌쭐쭘쭙쭝쭤쭸쭹쮜쮸쯔쯤쯧쯩찌찍찐찔찜찝찡찢찧차착찬찮찰참찹찻찼창찾채책챈챌챔챕챗챘챙챠챤챦챨챰챵처척천철첨첩첫첬청체첵첸첼쳄쳅쳇쳉쳐쳔쳤쳬쳰촁초촉촌촐촘촙촛총촤촨촬촹최쵠쵤쵬쵭쵯쵱쵸춈추축춘출춤춥춧충춰췄췌췐취췬췰췸췹췻췽츄츈츌츔츙츠측츤츨츰츱츳층치칙친칟칠칡침칩칫칭카칵칸칼캄캅캇캉캐캑캔캘캠캡캣캤캥캬캭컁커컥컨컫컬컴컵컷컸컹케켁켄켈켐켑켓켕켜켠켤켬켭켯켰켱켸코콕콘콜콤콥콧콩콰콱콴콸쾀쾅쾌쾡쾨쾰쿄쿠쿡쿤쿨쿰쿱쿳쿵쿼퀀퀄퀑퀘퀭퀴퀵퀸퀼큄큅큇큉큐큔큘큠크큭큰클큼큽킁키킥킨킬킴킵킷킹타탁탄탈탉탐탑탓탔탕태택탠탤탬탭탯탰탱탸턍터턱턴털턺텀텁텃텄텅테텍텐텔템텝텟텡텨텬텼톄톈토톡톤톨톰톱톳통톺톼퇀퇘퇴퇸툇툉툐투툭툰툴툼툽툿퉁퉈퉜퉤튀튁튄튈튐튑튕튜튠튤튬튱트특튼튿틀틂틈틉틋틔틘틜틤틥티틱틴틸팀팁팃팅파팍팎판팔팖팜팝팟팠팡팥패팩팬팰팸팹팻팼팽퍄퍅퍼퍽펀펄펌펍펏펐펑페펙펜펠펨펩펫펭펴편펼폄폅폈평폐폘폡폣포폭폰폴폼폽폿퐁퐈퐝푀푄표푠푤푭푯푸푹푼푿풀풂품풉풋풍풔풩퓌퓐퓔퓜퓟퓨퓬퓰퓸퓻퓽프픈플픔픕픗피픽핀필핌핍핏핑하학한할핥함합핫항해핵핸핼햄햅햇했행햐향허헉헌헐헒험헙헛헝헤헥헨헬헴헵헷헹혀혁현혈혐협혓혔형혜혠혤혭호혹혼홀홅홈홉홋홍홑화확환활홧황홰홱홴횃횅회획횐횔횝횟횡효횬횰횹횻후훅훈훌훑훔훗훙훠훤훨훰훵훼훽휀휄휑휘휙휜휠휨휩휫휭휴휵휸휼흄흅흉흐흑흔흖흗흘흙흠흡흣흥흩희흰흴흼흽힁히힉힌힘힐힙힛
    '가각간갇갈갉갊감갑값갓갔강갖갗같갚갛개객갠갤갬갭갯갰갱갸갹갼걀걋걍걔걘걜거걱건걷걸걺검겁것겄겅겆겉겊겋게겐겔겜겝겟겠겡겨격겪견겯결겸겹겻겼경곁계곈곌곕곗고곡곤곧골곪곬곯곰곱곳공곶과곽관괄괆괌괍괏광괘괜괠괩괬괭괴괵괸괼굄굅굇굉교굔굘굡굣구국군굳굴굵굶굻굼굽굿궁궂궈궉권궐궜궝궤궷귀귁귄귈귐귑귓규균귤그극근귿글긁금급긋긍긔기긱긴긷길긺김깁깃깅깆깊까깍깎깐깔깖깜깝깟깠깡깥깨깩깬깰깸깹깻깼깽꺄꺅꺌꺼꺽꺾껀껄껌껍껏껐껑께껙껜껨껫껭껴껸껼꼇꼈꼍꼐꼬꼭꼰꼲꼴꼼꼽꼿꽁꽂꽃꽈꽉꽐꽜꽝꽤꽥꽹꾀꾄꾈꾐꾑꾕꾜꾸꾹꾼꿀꿇꿈꿉꿋꿍꿎꿔꿜꿨꿩꿰꿱꿴꿸뀀뀁뀄뀌뀐뀔뀜뀝뀨끄끅끈끊끌끎끓끔끕끗끙끝끼끽낀낄낌낍낏낑나낙낚난낟날낡낢남납낫났낭낮낯낱낳내낵낸낼냄냅냇냈냉냐냑냔냘냠냥너넉넋넌널넒넓넘넙넛넜넝넣네넥넨넬넴넵넷넸넹녀녁년녈념녑녔녕녘녜녠노녹논놀놂놈놉놋농높놓놔놘놜놨뇌뇐뇔뇜뇝뇟뇨뇩뇬뇰뇹뇻뇽누눅눈눋눌눔눕눗눙눠눴눼뉘뉜뉠뉨뉩뉴뉵뉼늄늅늉느늑는늘늙늚늠늡늣능늦늪늬늰늴니닉닌닐닒님닙닛닝닢다닥닦단닫달닭닮닯닳담답닷닸당닺닻닿대댁댄댈댐댑댓댔댕댜더덕덖던덛덜덞덟덤덥덧덩■덫덮데덱덴델뎀뎁뎃뎄뎅뎌뎐뎔뎠뎡뎨뎬도독돈돋돌돎돐돔돕돗동돛돝돠돤돨돼됐되된될됨됩됫됴두둑둔둘둠둡둣둥둬뒀뒈뒝뒤뒨뒬뒵뒷뒹듀듄듈듐듕드득든듣들듦듬듭듯등듸디딕딘딛딜딤딥딧딨딩딪따딱딴딸■땀땁땃땄땅땋때땍땐땔땜땝땟땠땡떠떡떤떨떪떫떰떱떳떴떵떻떼떽뗀뗄뗌뗍뗏뗐뗑뗘뗬또똑똔똘똥똬똴뙈뙤뙨뚜뚝뚠뚤뚫뚬뚱뛔뛰뛴뛸뜀뜁뜅뜨뜩뜬뜯뜰뜸뜹뜻띄띈띌띔띕띠띤띨띰띱띳띵라락란랄람랍랏랐랑랒랖랗■래랙랜랠램랩랫랬랭랴략랸럇량러럭런럴럼럽럿렀렁렇레렉렌렐렘렙렛렝려력련렬렴렵렷렸령례롄롑롓로록론롤롬롭롯롱롸롼뢍뢨뢰뢴뢸룀룁룃룅료룐룔룝룟룡루룩룬룰룸룹룻룽뤄뤘뤠뤼뤽륀륄륌륏륑류륙륜률륨륩■륫륭르륵른를름릅릇릉릊릍릎리릭린릴림립릿링마막만많맏말맑맒맘맙맛망맞맡맣매맥맨맬맴맵맷맸맹맺먀먁먈먕머먹먼멀멂멈멉멋멍멎멓메멕멘멜멤멥멧멨멩며멱면멸몃몄명몇몌모목몫몬몰몲몸몹못몽뫄뫈뫘뫙뫼묀묄묍묏묑묘묜묠묩묫무묵묶문묻물묽묾뭄뭅뭇뭉뭍뭏뭐뭔뭘뭡뭣뭬뮈뮌뮐뮤뮨뮬뮴뮷므믄믈믐믓미믹민믿밀밂밈밉밋밌밍및밑바박밖밗반받발밝밞밟밤밥밧방밭배백밴밸뱀뱁뱃뱄뱅뱉뱌뱍뱐뱝버벅번벋벌벎범법벗벙벚베벡벤벧벨벰벱벳벴벵벼벽변별볍볏볐병볕볘볜보복볶본볼봄봅봇봉봐봔봤봬뵀뵈뵉뵌뵐뵘뵙뵤뵨부북분붇불붉붊붐붑붓붕붙붚붜붤붰붸뷔뷕뷘뷜뷩뷰뷴뷸븀븃븅브븍븐블븜븝븟비빅빈빌빎빔빕빗빙빚빛빠빡빤빨빪빰빱빳빴빵빻빼빽뺀뺄뺌뺍뺏뺐뺑뺘뺙뺨뻐뻑뻔뻗뻘뻠뻣뻤뻥뻬뼁뼈뼉뼘뼙뼛뼜뼝뽀뽁뽄뽈뽐뽑뽕뾔뾰뿅뿌뿍뿐뿔뿜뿟뿡쀼쁑쁘쁜쁠쁨쁩삐삑삔삘삠삡삣삥사삭삯산삳살삵삶삼삽삿샀상샅새색샌샐샘샙샛샜생샤샥샨샬샴샵샷샹섀섄섈섐섕서석섞섟선섣설섦섧섬섭섯섰성섶세섹센셀셈셉셋셌셍셔셕션셜셤셥셧셨셩셰셴셸솅소속솎손솔솖솜솝솟송솥솨솩솬솰솽쇄쇈쇌쇔쇗쇘쇠쇤쇨쇰쇱쇳쇼쇽숀숄숌숍숏숑수숙순숟술숨숩숫숭숯숱숲숴쉈쉐쉑쉔쉘쉠쉥쉬쉭쉰쉴쉼쉽쉿슁슈슉슐슘슛슝스슥슨슬슭슴습슷승시식신싣실싫심십싯싱싶싸싹싻싼쌀쌈쌉쌌쌍쌓쌔쌕쌘쌜쌤쌥쌨쌩썅써썩썬썰썲썸썹썼썽쎄쎈쎌쏀쏘쏙쏜쏟쏠쏢쏨쏩쏭쏴쏵쏸쐈쐐쐤쐬쐰쐴쐼쐽쑈쑤쑥쑨쑬쑴쑵쑹쒀쒔쒜쒸쒼쓩쓰쓱쓴쓸쓺쓿씀씁씌씐씔씜씨씩씬씰씸씹씻씽아악안앉않알앍앎앓암압앗았앙앝앞애액앤앨앰앱앳앴앵야약얀얄얇얌얍얏양얕얗얘얜얠얩어억언얹얻얼얽얾엄업없엇었엉엊엌엎에엑엔엘엠엡엣엥여역엮연열엶엷염엽엾엿였영옅옆옇예옌옐옘옙옛옜오옥온올옭옮옰옳옴옵옷옹옻와왁완왈왐왑왓왔왕왜왝왠왬왯왱외왹왼욀욈욉욋욍요욕욘욜욤욥욧용우욱운울욹욺움웁웃웅워웍원월웜웝웠웡웨웩웬웰웸웹웽위윅윈윌윔윕윗윙유육윤율윰윱윳융윷으윽은을읊음읍읏응읒읓읔읕읖읗의읜읠읨읫이익인일읽읾잃임입잇있잉잊잎자작잔잖잗잘잚잠잡잣잤장잦재잭잰잴잼잽잿쟀쟁쟈쟉쟌쟎쟐쟘쟝쟤쟨쟬저적전절젊점접젓정젖제젝젠젤젬젭젯젱져젼졀졈졉졌졍졔조족존졸졺좀좁좃종좆좇좋좌좍좔좝좟좡좨좼좽죄죈죌죔죕죗죙죠죡죤죵주죽준줄줅줆줌줍줏중줘줬줴쥐쥑쥔쥘쥠쥡쥣쥬쥰쥴쥼즈즉즌즐즘즙즛증지직진짇질짊짐집짓징짖짙짚짜짝짠짢짤짧짬짭짯짰짱째짹짼쨀쨈쨉쨋쨌쨍쨔쨘쨩쩌쩍쩐쩔쩜쩝쩟쩠쩡쩨쩽쪄쪘쪼쪽쫀쫄쫌쫍쫏쫑쫓쫘쫙쫠쫬쫴쬈쬐쬔쬘쬠쬡쭁쭈쭉쭌쭐쭘쭙쭝쭤쭸쭹쮜쮸쯔쯤쯧쯩찌찍찐찔찜찝찡찢찧차착찬찮찰참찹찻찼창찾채책챈챌챔챕챗챘챙챠챤챦챨챰챵처척천철첨첩첫첬청체첵첸첼쳄쳅쳇쳉쳐쳔쳤쳬쳰촁초촉촌촐촘촙촛총촤촨촬촹최쵠쵤쵬쵭쵯쵱쵸춈추축춘출춤춥춧충춰췄췌췐취췬췰췸췹췻췽츄츈츌츔츙츠측츤츨츰츱츳층치칙친칟칠칡침칩칫칭카칵칸칼캄캅캇캉캐캑캔캘캠캡캣캤캥캬캭컁커컥컨컫컬컴컵컷컸컹케켁켄켈켐켑켓켕켜켠켤켬켭켯켰켱켸코콕콘콜콤콥콧콩콰콱콴콸쾀쾅쾌쾡쾨쾰쿄쿠쿡쿤쿨쿰쿱쿳쿵쿼퀀퀄퀑퀘퀭퀴퀵퀸퀼큄큅큇큉큐큔큘큠크큭큰클큼큽킁키킥킨킬킴킵킷킹타탁탄탈탉탐탑탓탔탕태택탠탤탬탭탯탰탱탸턍터턱턴털턺텀텁텃텄텅테텍텐텔템텝텟텡텨텬텼톄톈토톡톤톨톰톱톳통톺톼퇀퇘퇴퇸툇툉툐투툭툰툴툼툽툿퉁퉈퉜퉤튀튁튄튈튐튑튕튜튠튤튬튱트특튼튿틀틂틈틉틋틔틘틜틤틥티틱틴틸팀팁팃팅파팍팎판팔팖팜팝팟팠팡팥패팩팬팰팸팹팻팼팽퍄퍅퍼퍽펀펄펌펍펏펐펑페펙펜펠펨펩펫펭펴편펼폄폅폈평폐폘폡폣포폭폰폴폼폽폿퐁퐈퐝푀푄표푠푤푭푯푸푹푼푿풀풂품풉풋풍풔풩퓌퓐퓔퓜퓟퓨퓬퓰퓸퓻퓽프픈플픔픕픗피픽핀필핌핍핏핑하학한할핥함합핫항해핵핸핼햄햅햇했행햐향허헉헌헐헒험헙헛헝헤헥헨헬헴헵헷헹혀혁현혈혐협혓혔형혜혠혤혭호혹혼홀홅홈홉홋홍홑화확환활홧황홰홱홴횃횅회획횐횔횝횟횡효횬횰횹횻후훅훈훌훑훔훗훙훠훤훨훰훵훼훽휀휄휑휘휙휜휠휨휩휫휭휴휵휸휼흄흇흉흐흑흔흖흗흘흙흠흡흣흥흩희흰흴흼흽힁히힉힌힐힘힙힛힝

    Public bitm(4) As Bitmap
    Public blank64 As Bitmap = Bitmap.FromFile("blank64.bmp")
    Public recpos As UInt32(,)
    Public picpos As UInt32(,,)
    Public res As Byte()()
    Public mem(4) As MemoryStream

    Public picnum As UInteger = 3490

    Public filename, nobu14res As String
    Public nobu14 As String
    Public resfile As Stream

    Public gene(34) As ListViewItem
    Public numindex As Integer = 0
    Public genindex As Integer = 0

    'Public gt64(&H4035), gt32(&H1035), gt512(&H100035), gt128(&H10035) As Byte

    Public neogendir As String = ""

    Public folder As New FolderBrowserDialog
    Public min, max As Integer
    Public genlist64 As New ImageList()
    Public item() As ListViewItem = New ListViewItem(99) {}
    Public chked(3489) As Byte

    Public kaozip As ZipFile = New ZipFile()
    Public imgtype As String = ""

    Dim lv2s As Integer
    Dim kaon As Integer
    Dim imgcheck(4) As Byte

    Dim rscptr As IntPtr = Marshal.AllocHGlobal(&H80000)
    Dim olen As Int32
    Dim msg As Int32
    Dim sumdiff As Int32 = 0

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SendMessage(Me.ListView1.Handle, LVM_SETICONSPACING, 0, 70 * 65536 + 54)
        SendMessage(Me.ListView3.Handle, LVM_SETICONSPACING, 0, 70 * 65536 + 70)

        RadioButton1.Checked = True

        With DataGridView1

            .Columns.Clear()
            .RowCount = 6
            .ColumnCount = 2


            .Columns(0).Width = 67
            .Columns(1).Width = 70
            .Columns(0).ReadOnly = True
            .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Rows(0).Cells(0).Value = "성"
            .Rows(1).Cells(0).Value = "이름"
            .Rows(2).Cells(0).Value = "통솔"
            .Rows(3).Cells(0).Value = "무력"
            .Rows(4).Cells(0).Value = "지력"
            .Rows(5).Cells(0).Value = "정치"

        End With

        For i = 0 To 4

            imgcheck(i) = 0

        Next

        Dim cfgfile As String = ""

        If File.Exists("Neo Editor.cfg") Then

            nobu14 = File.ReadAllText("Neo Editor.cfg")

            If Directory.Exists(nobu14) = False Then

                File.Delete("Neo Editor.cfg")
                folder_select()

            End If

        Else

            folder_select()

        End If

        nobu14res = nobu14 & "\res_grp.bin"
        loadpos()

        For i = 0 To 3489

            If picpos(0, i, 1) <= &H130 Then

                chked(i) = 0

            End If

        Next

        drawgeneral(0)

        For i = 0 To 34

            gene(i) = New ListViewItem(String.Format("{0:D4}", i * 100 + 1) & "번 ~ " & String.Format("{0:D4}", (i + 1) * 100) & "번", 0)

        Next

        ListView2.CheckBoxes = True

        ListView2.Columns.Add("무장", 119, HorizontalAlignment.Left)

        ListView2.Items.AddRange(gene)

        PictureBox1.Image = Image.FromFile("blank1.png")
        PictureBox2.Image = Image.FromFile("blank2.png")
        PictureBox3.Image = Image.FromFile("blank3.png")
        PictureBox4.Image = Image.FromFile("blank4.png")
        PictureBox5.Image = Image.FromFile("blank5.png")

    End Sub

    Private Sub folder_select()

        Dim FolderBrowserDialog1 As New FolderBrowserDialog

        FolderBrowserDialog1.ShowNewFolderButton = False
        FolderBrowserDialog1.Description = "창조가 설치된 폴더를 선택하십시오."

        If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then

            nobu14 = FolderBrowserDialog1.SelectedPath
            File.WriteAllText("Neo Editor.cfg", nobu14)

            If nobu14 Is Nothing Then

                Dispose()

            End If

        End If

    End Sub

    Public Sub loadpos()

        Dim buffer(3) As Byte

        ReDim recpos(17, 1)
        ReDim picpos(4, picnum - 1, 1)
        ReDim res(5)

        resfile = File.Open(nobu14res, FileMode.Open)
        resfile.Position = &H110

        For i = 0 To 16

            resfile.Read(buffer, 0, 4)
            recpos(i, 0) = BitConverter.ToUInt32(buffer, 0)

            resfile.Read(buffer, 0, 4)
            recpos(i, 1) = BitConverter.ToUInt32(buffer, 0)

        Next

        recpos(17, 1) = resfile.Length - recpos(5, 0)

        For i = 0 To 5

            If i <> 5 Then

                ReDim res(i)(recpos(i, 1) - 1)
                resfile.Position = recpos(i, 0)
                resfile.Read(res(i), 0, recpos(i, 1))

                mem(i) = New MemoryStream(res(i))

            Else

                ReDim res(5)(recpos(17, 1) - 1)
                resfile.Position = recpos(5, 0)
                resfile.Read(res(5), 0, recpos(17, 1))

            End If

        Next

        For i = 0 To 4

            mem(i).Position = &H20

            For j = 0 To picnum - 1

                mem(i).Read(buffer, 0, 4)
                picpos(i, j, 0) = BitConverter.ToUInt32(buffer, 0)

                mem(i).Read(buffer, 0, 4)
                picpos(i, j, 1) = BitConverter.ToUInt32(buffer, 0)

            Next

        Next

    End Sub

    Private Sub ListView2_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles ListView2.ItemCheck

        If e.Index <> 34 Then

            If ListView2.Items(e.Index).Checked = False Then

                For i = 0 To 99

                    If picpos(4, e.Index * 100 + i, 1) > &H130 Then

                        chked(e.Index * 100 + i) = 1

                    End If

                Next

            Else

                For i = 0 To 99

                    If picpos(4, e.Index * 100 + i, 1) > &H130 Then

                        chked(e.Index * 100 + i) = 0

                    End If

                Next

            End If


        Else

            If ListView2.Items(e.Index).Checked = False Then

                For i = 0 To 89

                    If picpos(4, e.Index * 100 + i, 1) > &H130 Then

                        chked(e.Index * 100 + i) = 1

                    End If

                Next

            Else

                For i = 0 To 89

                    If picpos(4, e.Index * 100 + i, 1) > &H130 Then

                        chked(e.Index * 100 + i) = 0

                    End If

                Next

            End If

        End If

        checkcount()

        If e.Index = ListView2.FocusedItem.Index Then

            drawgeneral(e.Index)

        End If

    End Sub

    Private Sub ListView2_ItemSelectionChanged(ByVal sender As System.Object, ByVal e As ListViewItemSelectionChangedEventArgs) Handles ListView2.ItemSelectionChanged

        numindex = e.ItemIndex * 100
        drawgeneral(e.ItemIndex)
        ListView1.Select()
        lv2s = e.ItemIndex

    End Sub

    Private Sub drawgeneral(ByVal ir As Integer)

        ListView1.Clear()

        If ir < 34 Then

            For i = 0 To 99

                item(i) = New ListViewItem(String.Format("{0:D4}", i + ir * 100 + 1), i)

            Next

        Else

            For i = 0 To 89

                item(i) = New ListViewItem(String.Format("{0:D4}", i + ir * 100 + 1), i)

            Next

        End If

        ListView1.Items.AddRange(item)

        Dim imagelist64 As New ImageList()
        imagelist64.ImageSize = New Size(48, 48)
        imagelist64.ColorDepth = ColorDepth.Depth32Bit

        Dim rect As Rectangle

        rect.X = 0
        rect.Y = 0
        rect.Width = 48
        rect.Height = 48

        If ir < 34 Then

            For i = 0 To 99

                If picpos(0, i + ir * 100, 1) > &H130 Then

                    imagelist64.Images.Add(makebmp(4, i + ir * 100).Clone(rect, PixelFormat.Format32bppArgb))

                Else

                    imagelist64.Images.Add(blank64.Clone(rect, PixelFormat.Format32bppArgb))

                End If

            Next

        Else

            For i = 0 To 89

                If picpos(0, i + ir * 100, 1) > &H130 Then

                    imagelist64.Images.Add(makebmp(4, i + ir * 100).Clone(rect, PixelFormat.Format32bppArgb))

                Else

                    imagelist64.Images.Add(blank64.Clone(rect, PixelFormat.Format32bppArgb))

                End If

            Next

        End If

        ListView1.LargeImageList = imagelist64

        If ir < 34 Then

            For i = 0 To 99

                If chked(i + ir * 100) = 1 Then

                    ListView1.Items.Item(i).Checked = True

                End If

            Next

        Else

            For i = 0 To 89

                If chked(i + ir * 100) = 1 Then

                    ListView1.Items.Item(i).Checked = True

                End If

            Next

        End If

    End Sub

    Private Sub loadzip()

        Dim pathname As String
        Dim imagei As Integer = 0
        Dim bmp As Bitmap

        Dim rect As Rectangle

        rect.X = 0
        rect.Y = 0
        rect.Width = 48
        rect.Height = 48

        ListView3.Items.Clear()
        ListView3.Clear()
        genlist64.Dispose()

        genlist64.ImageSize = New Size(48, 48)
        genlist64.ColorDepth = ColorDepth.Depth32Bit

        Dim bit64 As Bitmap

        Dim genimage() As ListViewItem = New ListViewItem() {}

        kaozip = New ZipFile(neogendir, System.Text.Encoding.GetEncoding(949))


        For Each entry As ZipEntry In kaozip

            If entry.FileName.ToLower().Contains("00037_") Then

                Dim kaostr As MemoryStream = New MemoryStream()

                entry.Extract(kaostr)

                Dim pathfrag As String() = entry.FileName.Split("/")

                If pathfrag.Length = 1 Then

                    Dim neofrag As String() = neogendir.Split("\")

                    pathname = neofrag(neofrag.Length - 1).Remove(neofrag(neofrag.Length - 1).Length - 4, 4)

                Else

                    pathname = pathfrag(pathfrag.Length - 2)

                End If

                ReDim Preserve genimage(imagei)

                genimage(imagei) = New ListViewItem(pathname, imagei)
                imagei += 1

                bmp = Bitmap.FromStream(kaostr)
                bit64 = bmp.Clone(New Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb)

                genlist64.Images.Add(bit64.Clone(rect, PixelFormat.Format32bppArgb))

                kaostr.Dispose()

            End If

        Next

        ListView3.Items.AddRange(genimage)

        ListView3.LargeImageList = genlist64

    End Sub

    Private Sub loadbmp()

        Dim pathname As String
        Dim imagei As Integer = 0
        Dim rect As Rectangle
        Dim bmp As Bitmap

        rect.X = 0
        rect.Y = 0
        rect.Width = 48
        rect.Height = 48

        Dim neofrag As String() = neogendir.Split("\")
        neogendir = ""

        For i = 0 To neofrag.Length - 3

            neogendir &= neofrag(i) & "\"

        Next

        ListView3.Items.Clear()
        ListView3.Clear()
        genlist64.Dispose()

        genlist64.ImageSize = New Size(48, 48)
        genlist64.ColorDepth = ColorDepth.Depth32Bit

        Dim bit64 As Bitmap

        Dim genimage() As ListViewItem = New ListViewItem() {}

        For Each imagedir As String In Directory.GetDirectories(neogendir)

            For Each files As String In Directory.GetFiles(imagedir)

                If files.Contains("00037_") Then

                    Dim pathfrag As String() = imagedir.Split("\")

                    pathname = pathfrag(pathfrag.Length - 1)

                    ReDim Preserve genimage(imagei)

                    genimage(imagei) = New ListViewItem(pathname, imagei)
                    imagei += 1

                    bmp = Bitmap.FromFile(files)
                    bit64 = bmp.Clone(New Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb)
                    genlist64.Images.Add(bit64.Clone(rect, PixelFormat.Format32bppArgb))

                End If

            Next

        Next

        ListView3.Items.AddRange(genimage)

        ListView3.LargeImageList = genlist64

    End Sub

    Private Sub loadbmp2()

        Dim pathname As String
        Dim imagei As Integer = 0
        Dim rect As Rectangle
        Dim bmp As Bitmap

        rect.X = 0
        rect.Y = 0
        rect.Width = 48
        rect.Height = 48

        Dim neofrag As String() = neogendir.Split("\")
        neogendir = ""

        For i = 0 To neofrag.Length - 2

            neogendir &= neofrag(i) & "\"

        Next

        ListView3.Items.Clear()
        ListView3.Clear()
        genlist64.Dispose()

        genlist64.ImageSize = New Size(48, 48)
        genlist64.ColorDepth = ColorDepth.Depth32Bit

        Dim bit64 As Bitmap

        Dim genimage() As ListViewItem = New ListViewItem() {}


        For Each files As String In Directory.GetFiles(neogendir)

            If files.Contains("00037_") Then

                Dim pathfrag As String() = files.Split("\")

                pathname = pathfrag(pathfrag.Length - 1)

                pathname = pathname.Remove(0, 6)
                pathname = pathname.Remove(pathname.Length - 4, 4)

                ReDim Preserve genimage(imagei)

                genimage(imagei) = New ListViewItem(pathname, imagei)
                imagei += 1

                bmp = Bitmap.FromFile(files)
                bit64 = bmp.Clone(New Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb)
                genlist64.Images.Add(bit64.Clone(rect, PixelFormat.Format32bppArgb))

            End If

        Next

        ListView3.Items.AddRange(genimage)

        ListView3.LargeImageList = genlist64

    End Sub

    Private Function writegen(ByVal geni As Integer) As Boolean

        Dim diff As Int32
        Dim reslw() As Byte
        Dim back() As Byte
        Dim orilen As Int32
        Dim buffer(3) As Byte

        For i = 0 To 4

            If imgcheck(i) = 1 Then

                reslw = makeres(i + 1)

                diff = CInt(olen) - CInt(picpos(i, geni, 1))
                sumdiff += diff

                orilen = recpos(i, 1) + diff

                ReDim back(res(i).Length - picpos(i, geni + 1, 0) - 1)

                Array.ConstrainedCopy(res(i), picpos(i, geni + 1, 0), back, 0, back.Length)

                ReDim Preserve res(i)(orilen - 1)

                For j = i To 16

                    If j <> i Then

                        recpos(j, 0) += diff

                    Else

                        recpos(j, 1) += diff

                    End If

                Next

                For j = geni To picnum - 1

                    If j <> geni Then

                        picpos(i, j, 0) += diff

                        buffer = BitConverter.GetBytes(picpos(i, j, 0))

                        Array.ConstrainedCopy(buffer, 0, res(i), j * 8 + &H20, 4)

                    Else

                        picpos(i, j, 1) += diff

                        buffer = BitConverter.GetBytes(picpos(i, j, 1))

                        Array.ConstrainedCopy(buffer, 0, res(i), j * 8 + &H24, 4)

                        Array.ConstrainedCopy(reslw, 0, res(i), picpos(i, j, 0), reslw.Length)

                    End If

                Next

                Array.ConstrainedCopy(back, 0, res(i), picpos(i, geni + 1, 0), back.Length)

            End If

        Next

        Return True

    End Function

    Private Function BmpToBytes(ByVal bmp As Bitmap) As Byte()

        Dim bData As Imaging.BitmapData = bmp.LockBits(New Rectangle(New Point(), bmp.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb)

        ' number of bytes in the bitmap
        Dim byteCount As Integer = bData.Stride * bmp.Height
        Dim bmpBytes(byteCount - 1) As Byte

        'Copy the locked bytes from memory
        System.Runtime.InteropServices.Marshal.Copy(bData.Scan0, bmpBytes, 0, byteCount)

        ' don't forget to unlock the bitmap!!
        bmp.UnlockBits(bData)

        Return bmpBytes

    End Function

    Private Sub savekao(ByVal i As Integer)

        Dim kaonum As String = "temp\" & String.Format("{0:D4}", i + 1)
        Dim bmp As Bitmap

        Directory.CreateDirectory(kaonum)

        If picpos(0, i, 1) > &H130 Then

            bmp = makebmp(0, i)
            bmp.Save(kaonum & "\00033_" & String.Format("{0:D5}", i + 1) & ".bmp")

        End If

        If picpos(1, i, 1) > &H130 Then

            bmp = makebmp(1, i)
            bmp.Save(kaonum & "\00034_" & String.Format("{0:D5}", i + 1) & ".bmp")

        End If

        If picpos(2, i, 1) > &H130 Then

            bmp = makebmp(2, i)
            bmp.Save(kaonum & "\00035_" & String.Format("{0:D5}", i + 1) & ".bmp")

        End If

        If picpos(3, i, 1) > &H130 Then

            bmp = makebmp(3, i)
            bmp.Save(kaonum & "\00036_" & String.Format("{0:D5}", i + 1) & ".bmp")

        End If

        If picpos(4, i, 1) > &H130 Then

            bmp = makebmp(4, i)
            bmp.Save(kaonum & "\00037_" & String.Format("{0:D5}", i + 1) & ".bmp")

        End If

    End Sub

    Private Sub ListView1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.Click

        genindex = ListView1.FocusedItem.Index

        kaon = Val(ListView1.FocusedItem.Text) - 1

        If picpos(0, kaon, 1) > &H130 Then

            bitm(0) = makebmp(0, kaon)
            PictureBox1.Image = bitm(0)
            imgcheck(0) = 1

        Else

            PictureBox1.Image = Image.FromFile("blank1.png")
            imgcheck(0) = 0

        End If

        If picpos(1, kaon, 1) > &H130 Then

            bitm(1) = makebmp(1, kaon)
            PictureBox2.Image = bitm(1)
            imgcheck(1) = 1

        Else

            PictureBox2.Image = Image.FromFile("blank2.png")
            imgcheck(1) = 0

        End If

        If picpos(2, kaon, 1) > &H130 Then

            bitm(2) = makebmp(2, kaon)
            PictureBox3.Image = bitm(2)
            imgcheck(2) = 1

        Else

            PictureBox3.Image = Image.FromFile("blank3.png")
            imgcheck(2) = 0

        End If

        If picpos(3, kaon, 1) > &H130 Then

            bitm(3) = makebmp(3, kaon)
            PictureBox4.Image = bitm(3)
            imgcheck(3) = 1

        Else

            PictureBox4.Image = Image.FromFile("blank4.png")
            imgcheck(3) = 0

        End If

        If picpos(4, kaon, 1) > &H130 Then

            bitm(4) = makebmp(4, kaon)
            PictureBox5.Image = bitm(4)
            imgcheck(4) = 1

        Else

            PictureBox5.Image = Image.FromFile("blank5.png")
            imgcheck(4) = 0

        End If

        If picpos(4, kaon, 1) > &H130 Then

            If ListView1.FocusedItem.Checked = True Then

                ListView1.FocusedItem.Checked = False
                chked(Val(ListView1.FocusedItem.Text) - 1) = 0

            Else

                ListView1.FocusedItem.Checked = True
                chked(Val(ListView1.FocusedItem.Text) - 1) = 1

            End If

        End If

        checkcount()

        CheckBox1.Checked = False

    End Sub

    Private Sub checkcount()

        Dim count As Integer = 0

        For i = 0 To 3489

            If chked(i) = 1 Then

                count += 1

            End If

        Next

        TextBox10.Text = count

    End Sub

    Private Sub ListView3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView3.Click

        Dim fitem As ListViewItem = ListView3.FocusedItem
        Dim bmp As Bitmap

        For i = 0 To 4

            imgcheck(i) = 0

        Next

        If imgtype = "zip" Then

            For Each entry As ZipEntry In kaozip

                If entry.FileName.ToLower().Contains(fitem.Text.ToLower() & "/00037_") Or entry.FileName.Substring(0, 5).Contains("00037_") Then

                    Dim kaostr As MemoryStream = New MemoryStream()

                    entry.Extract(kaostr)

                    bmp = Bitmap.FromStream(kaostr)

                    Select Case bmp.PixelFormat

                        Case PixelFormat.Format24bppRgb

                            bitm(4) = bmp.Clone(New Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb)

                        Case Else

                            bitm(4) = bmp

                    End Select

                    PictureBox5.Image = bitm(4)

                    kaostr.Dispose()

                    imgcheck(4) = 1

                    Exit For

                End If

            Next

            For Each entry As ZipEntry In kaozip

                If entry.FileName.ToLower().Contains(fitem.Text.ToLower() & "/00036_") Or entry.FileName.Substring(0, 5).Contains("00036_") Then

                    Dim kaostr As MemoryStream = New MemoryStream()

                    entry.Extract(kaostr)

                    bmp = Bitmap.FromStream(kaostr)

                    Select Case bmp.PixelFormat

                        Case PixelFormat.Format24bppRgb

                            bitm(3) = bmp.Clone(New Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb)

                        Case Else

                            bitm(3) = bmp

                    End Select

                    PictureBox4.Image = bitm(3)

                    kaostr.Dispose()

                    imgcheck(3) = 1

                    Exit For

                End If

            Next

            For Each entry As ZipEntry In kaozip

                If entry.FileName.ToLower().Contains(fitem.Text.ToLower() & "/00035_") Or entry.FileName.Substring(0, 5).Contains("00035_") Then

                    Dim kaostr As MemoryStream = New MemoryStream()

                    entry.Extract(kaostr)

                    bmp = Bitmap.FromStream(kaostr)

                    Select Case bmp.PixelFormat

                        Case PixelFormat.Format24bppRgb

                            bitm(2) = bmp.Clone(New Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb)

                        Case Else

                            bitm(2) = bmp

                    End Select

                    PictureBox3.Image = bitm(2)

                    kaostr.Dispose()

                    imgcheck(2) = 1

                    Exit For

                End If

            Next

            For Each entry As ZipEntry In kaozip

                If entry.FileName.ToLower().Contains(fitem.Text.ToLower() & "/00033_") Or entry.FileName.Substring(0, 5).Contains("00033_") Then

                    Dim kaostr As MemoryStream = New MemoryStream()

                    entry.Extract(kaostr)

                    bmp = Bitmap.FromStream(kaostr)

                    Select Case bmp.PixelFormat

                        Case PixelFormat.Format24bppRgb

                            bitm(0) = bmp.Clone(New Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb)

                        Case Else

                            bitm(0) = bmp

                    End Select

                    PictureBox1.Image = bitm(0)

                    kaostr.Dispose()

                    imgcheck(0) = 1

                    Exit For

                End If

            Next

            For Each entry As ZipEntry In kaozip

                If entry.FileName.ToLower().Contains(fitem.Text.ToLower() & "/00034_") Or entry.FileName.Substring(0, 5).Contains("00034_") Then

                    Dim kaostr As MemoryStream = New MemoryStream()

                    entry.Extract(kaostr)

                    bmp = Bitmap.FromStream(kaostr)

                    Select Case bmp.PixelFormat

                        Case PixelFormat.Format24bppRgb

                            bitm(1) = bmp.Clone(New Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb)

                        Case Else

                            bitm(1) = bmp

                    End Select

                    PictureBox2.Image = bitm(1)

                    kaostr.Dispose()

                    imgcheck(1) = 1

                    Exit For

                End If

            Next

        End If

        If imgtype = "bmp" Then

            For Each pathname As String In Directory.GetDirectories(neogendir)

                Dim pathfrag() As String = pathname.Split("\")

                If pathfrag(pathfrag.Length - 1).Contains(fitem.Text) Then

                    For Each files As String In Directory.GetFiles(pathname)

                        If files.Contains("00033_") Then

                            bitm(0) = Bitmap.FromFile(files, PixelFormat.Format32bppArgb)
                            'bitm(0) = bmp.Clone(New Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb)
                            PictureBox1.Image = bitm(0)

                            imgcheck(0) = 1

                        End If

                        If files.Contains("00034_") Then

                            bitm(1) = Bitmap.FromFile(files, PixelFormat.Format32bppArgb)
                            'bitm(1) = bmp.Clone(New Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb)
                            PictureBox2.Image = bitm(1)

                            imgcheck(1) = 1

                        End If

                        If files.Contains("00035_") Then

                            bitm(2) = Bitmap.FromFile(files, PixelFormat.Format32bppArgb)
                            'bitm(2) = bmp.Clone(New Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb)
                            PictureBox3.Image = bitm(2)

                            imgcheck(2) = 1

                        End If

                        If files.Contains("00036_") Then

                            bitm(3) = Bitmap.FromFile(files, PixelFormat.Format32bppArgb)
                            'bitm(3) = bmp.Clone(New Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb)
                            PictureBox4.Image = bitm(3)

                            imgcheck(3) = 1

                        End If

                        If files.Contains("00037_") Then

                            bitm(4) = Bitmap.FromFile(files, PixelFormat.Format32bppArgb)
                            'bitm(4) = bmp.Clone(New Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb)
                            PictureBox5.Image = bitm(4)

                            imgcheck(4) = 1

                            Exit For

                        End If

                    Next

                End If

            Next

        End If

        If imgtype = "bmp2" Then

            For Each files As String In Directory.GetFiles(neogendir)

                If files.Contains(fitem.Text & ".bmp") Then

                    If files.Contains("00033_") Then

                        bitm(0) = Bitmap.FromFile(files, PixelFormat.Format32bppArgb)
                        'bitm(0) = bmp.Clone(New Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb)
                        PictureBox1.Image = bitm(0)

                        imgcheck(0) = 1

                    End If

                    If files.Contains("00034_") Then

                        bitm(1) = Bitmap.FromFile(files, PixelFormat.Format32bppArgb)
                        'bitm(1) = bmp.Clone(New Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb)
                        PictureBox2.Image = bitm(1)

                        imgcheck(1) = 1

                    End If

                    If files.Contains("00035_") Then

                        bitm(2) = Bitmap.FromFile(files, PixelFormat.Format32bppArgb)
                        'bitm(2) = bmp.Clone(New Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb)
                        PictureBox3.Image = bitm(2)

                        imgcheck(2) = 1

                    End If

                    If files.Contains("00036_") Then

                        bitm(3) = Bitmap.FromFile(files, PixelFormat.Format32bppArgb)
                        'bitm(3) = bmp.Clone(New Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb)
                        PictureBox4.Image = bitm(3)

                        imgcheck(3) = 1

                    End If

                    If files.Contains("00037_") Then

                        bitm(4) = Bitmap.FromFile(files, PixelFormat.Format32bppArgb)
                        'bitm(4) = bmp.Clone(New Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb)
                        PictureBox5.Image = bitm(4)

                        imgcheck(4) = 1

                        Exit For

                    End If

                End If

            Next

        End If

        If imgcheck(0) = 0 Then

            PictureBox1.Image = Image.FromFile("blank1.png")

        End If

        If imgcheck(1) = 0 Then

            PictureBox2.Image = Image.FromFile("blank2.png")

        End If

        If imgcheck(2) = 0 Then

            PictureBox3.Image = Image.FromFile("blank3.png")

        End If

        If imgcheck(3) = 0 Then

            PictureBox4.Image = Image.FromFile("blank4.png")

        End If

        If imgcheck(4) = 0 Then

            PictureBox5.Image = Image.FromFile("blank5.png")

        End If

        ListView1.Focus()
        ListView1.Select()

        CheckBox1.Checked = False

    End Sub

    Private Sub loadbitm(ByVal kaoname As String)

        Dim entry As ZipEntry = New ZipEntry

        If kaozip.EntryFileNames.Contains(kaoname & "/00033_0" & kaoname & ".bmp") Then

            entry = kaozip(kaoname & "/00033_0" & kaoname & ".bmp")

            Dim kaostr0 As MemoryStream = New MemoryStream()
            entry.Extract(kaostr0)

            bitm(0) = Bitmap.FromStream(kaostr0)
            imgcheck(0) = 1

            kaostr0.Close()

        Else : imgcheck(0) = 0

        End If

        If kaozip.EntryFileNames.Contains(kaoname & "/00034_0" & kaoname & ".bmp") Then

            entry = kaozip(kaoname & "/00034_0" & kaoname & ".bmp")

            Dim kaostr1 As MemoryStream = New MemoryStream()
            entry.Extract(kaostr1)

            bitm(1) = Bitmap.FromStream(kaostr1)
            imgcheck(1) = 1

            kaostr1.Close()

        Else : imgcheck(1) = 0

        End If

        If kaozip.EntryFileNames.Contains(kaoname & "/00035_0" & kaoname & ".bmp") Then

            entry = kaozip(kaoname & "/00035_0" & kaoname & ".bmp")

            Dim kaostr2 As MemoryStream = New MemoryStream()
            entry.Extract(kaostr2)

            bitm(2) = Bitmap.FromStream(kaostr2)
            imgcheck(2) = 1

            kaostr2.Close()

        Else : imgcheck(2) = 0

        End If

        If kaozip.EntryFileNames.Contains(kaoname & "/00036_0" & kaoname & ".bmp") Then

            entry = kaozip(kaoname & "/00036_0" & kaoname & ".bmp")

            Dim kaostr3 As MemoryStream = New MemoryStream()
            entry.Extract(kaostr3)

            bitm(3) = Bitmap.FromStream(kaostr3)
            imgcheck(3) = 1

            kaostr3.Close()

        Else : imgcheck(3) = 0

        End If

        If kaozip.EntryFileNames.Contains(kaoname & "/00037_0" & kaoname & ".bmp") Then

            entry = kaozip(kaoname & "/00037_0" & kaoname & ".bmp")

            Dim kaostr4 As MemoryStream = New MemoryStream()
            entry.Extract(kaostr4)

            bitm(4) = Bitmap.FromStream(kaostr4)
            imgcheck(4) = 1

            kaostr4.Close()

        Else : imgcheck(4) = 0

        End If

    End Sub

    Private Sub 신무장저장ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 신무장저장ToolStripMenuItem.Click

        Dim SaveFileDialog1 As New SaveFileDialog

        Dim buffer() As Byte
        Dim name As String
        Dim facenum As UInt16

        '&h34:성
        '&h52:이름
        '&h12D: 무력
        '&h131: 통솔
        '&h135: 지력
        '&h145: 정치

        With DataGridView1

            If .Rows(0).Cells(1).Value.ToString <> "" Then

                name = han2jis(.Rows(0).Cells(1).Value.ToString)

                buffer = Encoding.GetEncoding(932).GetBytes(name)
                Array.ConstrainedCopy(buffer, 0, neogen, &H34, buffer.Length)

            End If

            If .Rows(1).Cells(1).Value.ToString <> "" Then

                name = han2jis(.Rows(1).Cells(1).Value.ToString)

                buffer = Encoding.GetEncoding(932).GetBytes(name)
                Array.ConstrainedCopy(buffer, 0, neogen, &H52, buffer.Length)

            End If

            neogen(&H129) = Val(.Rows(2).Cells(1).Value)
            neogen(&H12D) = Val(.Rows(3).Cells(1).Value)
            neogen(&H131) = Val(.Rows(4).Cells(1).Value)
            neogen(&H135) = Val(.Rows(5).Cells(1).Value)

        End With

        Select Case kaon

            Case 0 To 1365

                facenum = kaon

            Case 1366 To 1389

                facenum = 4000 + kaon - 1366

            Case 1390 To 1403

                facenum = 5000 + kaon - 1390

            Case 1404 To 1441

                facenum = 6000 + kaon - 1404

            Case 1442 To 1457

                facenum = 7000 + kaon - 1442

            Case 1458 To 2357

                facenum = 7500 + kaon - 1858

            Case 2358 To 3354

                facenum = 8000 + kaon - 2358

            Case 3355 To 3371

                facenum = 9000 + kaon - 3355

            Case 3372 To 3489

                facenum = 9500 + kaon - 3372

        End Select

        Array.ConstrainedCopy(BitConverter.GetBytes(facenum), 0, neogen, &H10D, 2)

        SaveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\TecmoKoei\NOBU14\EDIT\BUSHOU"
        SaveFileDialog1.Filter = "14B files (*.14B)|*.14B"
        SaveFileDialog1.FilterIndex = 1
        SaveFileDialog1.RestoreDirectory = True

        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then

            File.WriteAllBytes(SaveFileDialog1.FileName, neogen)

        End If

    End Sub

    'Private Sub 전체백업ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 전체백업ToolStripMenuItem.Click

    '    Dim i As Integer

    '    If MsgBoxResult.Ok = MsgBox("전체 무장이미지를 백업합니다. 계속하시겠습니까?", MsgBoxStyle.OkCancel) Then

    '        Directory.CreateDirectory("temp")

    '        For i = 0 To 1999

    '            savekao(i)

    '        Next

    '        If File.Exists("원본무장.zip") Then

    '            File.Delete("원본무장.zip")

    '        End If

    '        kaozip.AddDirectory("temp")
    '        kaozip.Save("원본무장.zip")

    '        Directory.Delete("temp", True)

    '        MsgBox("완료되었습니다.")

    '    End If

    'End Sub

    Private Sub 한꺼번에추출ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 한꺼번에추출ToolStripMenuItem.Click

        Dim SaveFileDialog1 As New SaveFileDialog
        Dim zipname As String

        SaveFileDialog1.Filter = "zip files (*.zip)|*.zip"
        SaveFileDialog1.FilterIndex = 1
        SaveFileDialog1.RestoreDirectory = True

        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then

            If SaveFileDialog1.CheckFileExists = True Then

                File.Delete(SaveFileDialog1.FileName)

            End If

            zipname = SaveFileDialog1.FileName

            If File.Exists(zipname) Then

                File.Delete(zipname)

            End If

            kaozip.Initialize(zipname)
            kaozip = New ZipFile(zipname, System.Text.Encoding.GetEncoding(949))

            Directory.CreateDirectory("temp")

            For i = 0 To 3489

                If chked(i) = 1 Then

                    savekao(i)

                End If

            Next

            kaozip.AddDirectory("temp")
            kaozip.Save(zipname)

            Directory.Delete("temp", True)

            MsgBox("저장이 완료되었습니다.")

        End If

    End Sub

    Private Sub 한꺼번에적용ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 한꺼번에적용ToolStripMenuItem.Click

        Dim OpenFileDialog1 As New OpenFileDialog
        Dim pathname As String

        OpenFileDialog1.Filter = "zipfiles (*.zip)|*.zip|All files (*.*)|*.*"
        OpenFileDialog1.FilterIndex = 1
        OpenFileDialog1.RestoreDirectory = True

        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then

            If OpenFileDialog1.FileName.EndsWith("zip") Then

                kaozip = New ZipFile(OpenFileDialog1.FileName, System.Text.Encoding.GetEncoding(949))
                imgtype = "zip"

            End If

            'If OpenFileDialog1.FileName.EndsWith("nei") Then

            '    Dim kaotext As String = File.ReadAllText(OpenFileDialog1.FileName)
            '    imgtype = "nei"

            'End If

        End If

        If imgtype = "zip" Then

            Dim kaonumber(3489) As String
            Dim i As Integer = 0

            For Each entry1 As ZipEntry In kaozip

                If entry1.FileName.ToLower().Contains("/00037") Then

                    Dim pathfrag As String() = entry1.FileName.Split("/")
                    pathname = pathfrag(pathfrag.Length - 2)

                    kaonumber(i) = pathname

                    i += 1

                End If

            Next

            ReDim Preserve kaonumber(i - 1)

            For j = 0 To kaonumber.Length - 1

                loadbitm(kaonumber(j))
                writegen(Val(kaonumber(j)) - 1)

            Next

            'If MsgBoxResult.Ok = MsgBox("원본무장의 위치을 저장하시겠습니까?", MsgBoxStyle.OkCancel) Then

            '    Dim neisave As String = ""

            '    For j = 0 To kaonumber.Length - 1

            '        neisave &= kaonumber(j)

            '    Next

            '    If File.Exists(OpenFileDialog1.FileName.Remove(OpenFileDialog1.FileName.Length - 4) & ".nei") Then

            '        File.Delete(OpenFileDialog1.FileName.Remove(OpenFileDialog1.FileName.Length - 4) & ".nei")

            '    End If

            '    File.WriteAllText(OpenFileDialog1.FileName.Remove(OpenFileDialog1.FileName.Length - 4) & ".nei", neisave)

            'End If

            MsgBox("적용되었습니다.")

        End If

        'If imgtype = "nei" Then

        '    Dim kaoinfo As String = File.ReadAllText(OpenFileDialog1.FileName)
        '    Dim kaonumber(kaoinfo.Length / 4) As String

        '    For j = 0 To kaonumber.Length - 2

        '        kaonumber(j) = kaoinfo.Substring(j * 4, 4)

        '    Next

        '    kaozip = New ZipFile("원본무장.zip", System.Text.Encoding.GetEncoding(949))

        '    For j = 0 To kaonumber.Length - 2

        '        loadbitm(kaonumber(j))
        '        writegen(Val(kaonumber(j)))

        '    Next

        'End If

        drawgeneral(lv2s)
        ListView1.Focus()
        ListView1.Select()

    End Sub

    Private Sub 이미지폴더변경ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 이미지폴더변경ToolStripMenuItem.Click

        Dim ofd1 As OpenFileDialog = New OpenFileDialog()

        'If neogendir <> "" Then
        '    GoTo standby
        'End If

        'If neogendir = "" Then
        Dim filecount As Integer = 0
        Dim gendir As String = ""

        If ofd1.ShowDialog() = DialogResult.OK Then

            neogendir = ofd1.FileName
            Dim sdir() As String = neogendir.Split("\")

            For i = 0 To sdir.Length - 2

                gendir &= sdir(i)
                gendir &= "\"

            Next

            If neogendir.EndsWith("zip") = True Then

                imgtype = "zip"
                loadzip()

            End If

            If neogendir.ToLower().EndsWith("bmp") = True Or _
                neogendir.ToLower().EndsWith("png") = True Or _
                neogendir.ToLower().EndsWith("jpg") = True Then

                For Each files As String In Directory.GetFiles(gendir)

                    If files.Contains("00037") Then

                        filecount += 1

                    End If

                Next

                If filecount = 1 Then

                    imgtype = "bmp"
                    loadbmp()

                End If

                If filecount >= 2 Then

                    imgtype = "bmp2"
                    loadbmp2()

                End If

            End If

        Else

            Exit Sub

        End If

        'End If

    End Sub

    Private Function jis2han(ByRef jstr As String) As String

        jis2han = ""

        For i = 0 To jstr.Length - 1

            If Jisdic.indexof(jstr(i)) >= 0 Then

                jis2han &= kordic(Jisdic.indexof(jstr(i)))

            Else

                If Jisdic.IndexOf(jstr(i)) >= 0 Then

                    jis2han &= kordic(Jisdic.IndexOf(jstr(i)))

                Else

                    jis2han &= jstr(i)

                End If

            End If

        Next

    End Function

    Private Function han2jis(ByRef hstr As String) As String

        han2jis = ""

        For i = 0 To hstr.Length - 1

            If kordic.indexof(hstr(i)) >= 0 Then

                han2jis &= Jisdic(kordic.indexof(hstr(i)))

            Else

                han2jis &= hstr(i)

            End If

        Next

    End Function

    Private Function makebmp(ByVal resnum As Integer, ByVal kaonum As Integer) As Bitmap

        'nobu33: 256x256 240x240
        'nobu34: 512x1024 422x600
        'nobu35: 256x128 204x120
        'nobu36: 128x128 96x82
        'nobu37: 64x64 48x48

        Dim src As Byte() = decoderes(resnum, kaonum)
        Dim temp As Byte

        'Dim colormax(2) As Byte
        'Dim colormin(2) As Byte
        'Dim coltb(3) As Byte
        'Dim alpha(15) As Byte
        'Dim alphamax As Byte
        'Dim alphamin As Byte
        'Dim bitcode As UInt32
        Dim width, height As Integer

        Select Case resnum

            Case 0
                width = 256
                height = 256

            Case 1

                width = 512
                height = 1024

            Case 2

                width = 256
                height = 128

            Case 3

                width = 128
                height = 128

            Case 4

                width = 64
                height = 64

        End Select

        Dim bmp37(width * height * 4 - 1 + &H36) As Byte

        Array.ConstrainedCopy(ManagedSquish.SquishWrapper.DecompressImage(src, width, height, ManagedSquish.SquishFlags.Dxt5), 0, bmp37, &H36, bmp37.Length - &H36)


        Select Case resnum

            Case 0

                Array.ConstrainedCopy(header256, 0, bmp37, 0, &H36)

            Case 1

                Array.ConstrainedCopy(header512, 0, bmp37, 0, &H36)

            Case 2

                Array.ConstrainedCopy(header192, 0, bmp37, 0, &H36)

            Case 3

                Array.ConstrainedCopy(header128, 0, bmp37, 0, &H36)

            Case 4

                Array.ConstrainedCopy(header64, 0, bmp37, 0, &H36)

        End Select

        For i = 0 To (height \ 4) - 1

            For j = 0 To (width \ 4) - 1

                For k = 0 To 15

                    temp = bmp37((i * width * 4 + (k \ 4) * width + j * 4 + (k Mod 4)) * 4 + &H36)
                    bmp37((i * width * 4 + (k \ 4) * width + j * 4 + (k Mod 4)) * 4 + &H36) = bmp37((i * width * 4 + (k \ 4) * width + j * 4 + (k Mod 4)) * 4 + 2 + &H36)
                    bmp37((i * width * 4 + (k \ 4) * width + j * 4 + (k Mod 4)) * 4 + 2 + &H36) = temp

                Next

            Next

        Next

        'For i = 0 To (height \ 4) - 1

        '    For l = 0 To (width \ 4) - 1

        '        alphamax = src(i * width * 4 + l * 16 + 1)
        '        alphamin = src(i * width * 4 + l * 16)

        '        For m = 0 To 1

        '            bitcode = src(i * width * 4 + l * 16 + 2 + m * 3) _
        '                + src(i * width * 4 + l * 16 + 3 + m * 3) * &H100 + src(i * width * 4 + l * 16 + 4 + m * 3) * &H10000

        '            For n = 0 To 7

        '                alpha(m * 8 + n) = (bitcode >> (n * 3)) Mod 8

        '            Next

        '        Next

        '        For m = 0 To 15

        '            If alphamax >= alphamin Then

        '                Select Case alpha(m)

        '                    Case 0

        '                        alpha(m) = alphamin

        '                    Case 1

        '                        alpha(m) = alphamax

        '                    Case 2

        '                        alpha(m) = (alphamax + alphamin * 4) \ 5

        '                    Case 3

        '                        alpha(m) = (alphamax * 2 + alphamin * 3) \ 5

        '                    Case 4

        '                        alpha(m) = (alphamax * 3 + alphamin * 2) \ 5

        '                    Case 5

        '                        alpha(m) = (alphamax * 4 + alphamin) \ 5

        '                    Case 6

        '                        alpha(m) = 0

        '                    Case 7

        '                        alpha(m) = &HFF

        '                End Select

        '            Else

        '                Select Case alpha(m)

        '                    Case 0

        '                        alpha(m) = alphamin

        '                    Case 1

        '                        alpha(m) = alphamax

        '                    Case 2

        '                        alpha(m) = (alphamax + alphamin * 6) \ 7

        '                    Case 3

        '                        alpha(m) = (alphamax * 2 + alphamin * 5) \ 7

        '                    Case 4

        '                        alpha(m) = (alphamax * 3 + alphamin * 4) \ 7

        '                    Case 5

        '                        alpha(m) = (alphamax * 4 + alphamin * 3) \ 7

        '                    Case 6

        '                        alpha(m) = (alphamax * 5 + alphamin * 2) \ 7

        '                    Case 7

        '                        alpha(m) = (alphamax * 6 + alphamin) \ 7

        '                End Select

        '            End If


        '            bmp37((i * width * 4 + (m \ 4) * width + l * 4 + (m Mod 4)) * 4 + 3 + &H36) = alpha(m)

        '        Next

        '        colormax(2) = ((src(i * width * 4 + l * 16 + 9) >> 3) << 3)
        '        If colormax(2) > 0 Then
        '            colormax(2) -= 1
        '        End If

        '        colormax(1) = ((src(i * width * 4 + l * 16 + 9) << 5)) Or ((src(i * width * 4 + l * 16 + 8) >> 5) << 2)
        '        If colormax(1) > 0 Then
        '            colormax(1) -= 1
        '        End If

        '        colormax(0) = (src(i * width * 4 + l * 16 + 8) << 3)
        '        If colormax(0) > 0 Then
        '            colormax(0) -= 1
        '        End If

        '        colormin(2) = ((src(i * width * 4 + l * 16 + 11) >> 3) << 3)
        '        If colormin(2) > 0 Then
        '            colormin(2) -= 1
        '        End If

        '        colormin(1) = ((src(i * width * 4 + l * 16 + 11) << 5)) Or ((src(i * width * 4 + l * 16 + 10) >> 5) << 2)
        '        If colormin(1) > 0 Then
        '            colormin(1) -= 1
        '        End If

        '        colormin(0) = (src(i * width * 4 + l * 16 + 10) << 3)
        '        If colormin(0) > 0 Then
        '            colormin(0) -= 1
        '        End If

        '        For j = 0 To 3

        '            For k = 0 To 3

        '                coltb(j) = (src(i * width * 4 + l * 16 + 12 + k) >> (j * 2)) Mod 4

        '                Select Case coltb(j)

        '                    Case 0

        '                        bmp37((i * width * 4 + k * width + l * 4 + j) * 4 + &H36) = colormax(0)
        '                        bmp37((i * width * 4 + k * width + l * 4 + j) * 4 + 1 + &H36) = colormax(1)
        '                        bmp37((i * width * 4 + k * width + l * 4 + j) * 4 + 2 + &H36) = colormax(2)

        '                    Case 3

        '                        bmp37((i * width * 4 + k * width + l * 4 + j) * 4 + &H36) = (colormax(0) + colormin(0) * 2) \ 3
        '                        bmp37((i * width * 4 + k * width + l * 4 + j) * 4 + 1 + &H36) = (colormax(1) + colormin(1) * 2) \ 3
        '                        bmp37((i * width * 4 + k * width + l * 4 + j) * 4 + 2 + &H36) = (colormax(2) + colormin(2) * 2) \ 3

        '                    Case 2

        '                        bmp37((i * width * 4 + k * width + l * 4 + j) * 4 + &H36) = (colormax(0) * 2 + colormin(0)) \ 3
        '                        bmp37((i * width * 4 + k * width + l * 4 + j) * 4 + 1 + &H36) = (colormax(1) * 2 + colormin(1)) \ 3
        '                        bmp37((i * width * 4 + k * width + l * 4 + j) * 4 + 2 + &H36) = (colormax(2) * 2 + colormin(2)) \ 3

        '                    Case 1

        '                        bmp37((i * width * 4 + k * width + l * 4 + j) * 4 + &H36) = colormin(0)
        '                        bmp37((i * width * 4 + k * width + l * 4 + j) * 4 + 1 + &H36) = colormin(1)
        '                        bmp37((i * width * 4 + k * width + l * 4 + j) * 4 + 2 + &H36) = colormin(2)

        '                End Select

        '            Next
        '        Next
        '    Next
        'Next

        Dim mem512 As MemoryStream = New MemoryStream(bmp37)
        makebmp = Bitmap.FromStream(mem512)

        Return makebmp

    End Function

    Private Function decoderes(ByVal resnum As Integer, ByVal kaonum As Integer) As Byte()

        Dim rsc(&H80000 - 1) As Byte

        Dim src(picpos(resnum, kaonum, 1) - 1) As Byte

        Array.ConstrainedCopy(res(resnum), picpos(resnum, kaonum, 0), src, 0, picpos(resnum, kaonum, 1))

        LWMemoryDecode(src, src.Length, rscptr, olen, msg, Me.Handle)

        ReDim rsc(olen - 1 - &H38)

        Marshal.Copy(rscptr + &H38, rsc, 0, olen - &H38)

        Return rsc

    End Function

    Private Function makeres(ByVal bmpnum As Integer) As Byte()

        Dim src() As Byte

        Dim width As Integer
        Dim height As Integer

        Select Case bmpnum

            Case 1

                width = 256
                height = 256

                src = BmpToBytes(bitm(0))

                'ReDim src(width * height * 4 - 1)
                'Array.ConstrainedCopy(BmpToBytes(PictureBox1.Image), 0, src, 0, src.Length)

            Case 2

                width = 512
                height = 1024

                src = BmpToBytes(bitm(1))

                'ReDim src(width * height * 4 - 1)
                'Array.ConstrainedCopy(BmpToBytes(PictureBox2.Image), 0, src, 0, src.Length)

            Case 3

                width = 256
                height = 128

                src = BmpToBytes(bitm(2))

                'ReDim src(width * height * 4 - 1)
                'Array.ConstrainedCopy(BmpToBytes(PictureBox3.Image), 0, src, 0, src.Length)

            Case 4

                width = 128
                height = 128

                src = BmpToBytes(bitm(3))

                'ReDim src(width * height * 4 - 1)
                'Array.ConstrainedCopy(BmpToBytes(PictureBox4.Image), 0, src, 0, src.Length)

            Case 5

                width = 64
                height = 64

                src = BmpToBytes(bitm(4))

                'ReDim src(width * height * 4 - 1)
                'Array.ConstrainedCopy(BmpToBytes(PictureBox5.Image), 0, src, 0, src.Length)

        End Select

        'Dim alpha(15), diff(15) As Byte
        'Dim alphamax As Byte
        'Dim alphamin As Byte
        'Dim range(4) As Byte
        'Dim bitcode As UInt32
        'Dim alphacode(2) As Byte

        Dim resbyte(width * height - 1 + &H38) As Byte

        'For i = 0 To (height \ 4) - 1

        '    For j = 0 To (width \ 4) - 1

        '        For k = 0 To 15

        '            alpha(k) = src((i * width * 4 + (k \ 4) * width + j * 4 + (k Mod 4)) * 4 + 3)

        '        Next

        '        For k = 0 To 15

        '            diff(k) = alpha(k)

        '        Next

        '        Select Case alpha.Min()

        '            Case &HFF

        '                alphamin = 0
        '                alphamax = &HFF

        '            Case Else

        '                For k = 0 To 15

        '                    If diff(k) = &HFF Then

        '                        diff(k) = 0

        '                    End If

        '                Next

        '                alphamax = diff.Max()

        '        End Select

        '        For k = 0 To 15

        '            diff(k) = alpha(k)

        '        Next

        '        Select Case alpha.Max()

        '            Case 0

        '                alphamin = 0
        '                alphamax = &HFF

        '            Case Else

        '                For k = 0 To 15

        '                    If diff(k) = 0 Then

        '                        diff(k) = &HFF

        '                    End If

        '                Next

        '                alphamin = diff.Min()

        '        End Select

        '        If alphamin >= alphamax Then

        '            Select Case alphamin

        '                Case 0

        '                    alphamin = 0
        '                    alphamax = &HFF

        '                Case &HFF

        '                    alphamin = 0
        '                    alphamax = &HFF

        '                Case 1 To &H7F

        '                    alphamin = 0

        '                Case &H80 To &HFE

        '                    alphamax = &HFF

        '            End Select

        '        End If

        '        range(0) = (alphamin * 9 + alphamax) \ 10
        '        range(1) = (alphamin * 7 + alphamax * 3) \ 10
        '        range(2) = (alphamin \ 2 + alphamax \ 2)
        '        range(3) = (alphamin * 3 + alphamax * 7) \ 10
        '        range(4) = (alphamin + alphamax * 9) \ 10

        '        For k = 0 To 15

        '            Select Case alpha(k)

        '                Case 0

        '                    diff(k) = 6

        '                Case &HFF

        '                    diff(k) = 7

        '                Case alphamin To range(0)

        '                    diff(k) = 0

        '                Case range(0) + 1 To range(1)

        '                    diff(k) = 2

        '                Case range(1) + 1 To range(2)

        '                    diff(k) = 3

        '                Case range(2) + 1 To range(3)

        '                    diff(k) = 4

        '                Case range(3) + 1 To range(4)

        '                    diff(k) = 5

        '                Case range(4) + 1 To alphamax

        '                    diff(k) = 1

        '            End Select

        '        Next

        '        resbyte(i * width * 4 + j * 16 + &H38) = alphamin
        '        resbyte(i * width * 4 + j * 16 + 1 + &H38) = alphamax

        '        For l = 0 To 1

        '            bitcode = diff(l * 8) + diff(l * 8 + 1) * 8 + diff(l * 8 + 2) * 64 + diff(l * 8 + 3) * 512 + _
        '            diff(l * 8 + 4) * 4096 + diff(l * 8 + 5) * 32768 + diff(l * 8 + 6) * 262144 + diff(l * 8 + 7) * 2097152

        '            For m = 0 To 2

        '                alphacode(m) = (bitcode >> (m * 8)) Mod &H100

        '                resbyte(i * width * 4 + j * 16 + l * 3 + 2 + m + &H38) = alphacode(m)

        '            Next

        '        Next

        '    Next

        'Next

        'Dim color(15, 3) As Byte
        'Dim csum(2) As Integer

        'Dim colrange(2) As Int16

        'Dim count As Byte
        'Dim max(2) As Byte, min(2) As Byte

        'Dim colsum(15) As Integer
        'Dim maxp As Byte, minp As Byte

        'For i = 0 To (height \ 4) - 1

        '    For j = 0 To (width \ 4) - 1

        '        For k = 0 To 15

        '            color(k, 0) = src((i * width * 4 + (k \ 4) * width + j * 4 + (k Mod 4)) * 4)
        '            color(k, 1) = src((i * width * 4 + (k \ 4) * width + j * 4 + (k Mod 4)) * 4 + 1)
        '            color(k, 2) = src((i * width * 4 + (k \ 4) * width + j * 4 + (k Mod 4)) * 4 + 2)

        '        Next

        '        For k = 0 To 15

        '            colsum(k) = 0

        '            For l = 0 To 2

        '                colsum(k) += color(k, l)

        '            Next

        '        Next

        '        maxp = 0
        '        minp = 0

        '        Do Until colsum(maxp) = colsum.Max()

        '            maxp += 1

        '        Loop

        '        Do Until colsum(minp) = colsum.Min()

        '            minp += 1

        '        Loop

        '        colrange(0) = (colsum(minp) * 3 + colsum(maxp)) \ 4
        '        colrange(1) = (colsum(minp) + colsum(maxp)) \ 2
        '        colrange(2) = (colsum(minp) + colsum(maxp) * 3) \ 4

        '        For k = 0 To 15

        '            Select Case colsum(k)

        '                Case colsum(minp) To colrange(0)

        '                    color(k, 3) = 1

        '                Case colrange(0) To colrange(1)

        '                    color(k, 3) = 3

        '                Case colrange(1) To colrange(2)

        '                    color(k, 3) = 2

        '                Case colrange(2) To colsum(maxp)

        '                    color(k, 3) = 0

        '            End Select

        '        Next

        '        For l = 0 To 2

        '            csum(l) = 0
        '            count = 0

        '            For k = 0 To 15

        '                If color(k, 3) = 0 Then

        '                    csum(l) += color(k, l)
        '                    count += 1

        '                End If

        '            Next

        '            If count <> 0 Then

        '                max(l) = csum(l) \ count

        '            End If

        '        Next

        '        For l = 0 To 2

        '            csum(l) = 0
        '            count = 0

        '            For k = 0 To 15

        '                If color(k, 3) = 1 Then

        '                    csum(l) += color(k, l)
        '                    count += 1

        '                End If

        '            Next

        '            If count <> 0 Then

        '                min(l) = csum(l) \ count

        '            End If

        '        Next

        '        bitcode = 0
        '        bitcode += (max(0) >> 3)
        '        bitcode += ((max(1) >> 2) << 5)
        '        bitcode += ((max(1) >> 5) * &H100)
        '        bitcode += (((max(2) >> 3) << 3) * &H100)

        '        resbyte(i * width * 4 + j * 16 + 8 + &H38) = bitcode Mod &H100
        '        resbyte(i * width * 4 + j * 16 + 9 + &H38) = (bitcode >> 8) Mod &H100

        '        bitcode = 0
        '        bitcode += (min(0) >> 3)
        '        bitcode += ((min(1) >> 2) << 5)
        '        bitcode += ((min(1) >> 5) * &H100)
        '        bitcode += (((min(2) >> 3) << 3) * &H100)

        '        resbyte(i * width * 4 + j * 16 + 10 + &H38) = bitcode Mod &H100
        '        resbyte(i * width * 4 + j * 16 + 11 + &H38) = (bitcode >> 8) Mod &H100

        '        For k = 0 To 3

        '            bitcode = 0
        '            bitcode += color(k * 4, 3)
        '            bitcode += (color(k * 4 + 1, 3) * 4)
        '            bitcode += (color(k * 4 + 2, 3) * 16)
        '            bitcode += (color(k * 4 + 3, 3) * 64)

        '            resbyte(i * width * 4 + j * 16 + 12 + k + &H38) = bitcode

        '        Next
        '    Next
        'Next

        Dim temp As Byte

        For i = 0 To (height \ 4) - 1

            For j = 0 To (width \ 4) - 1

                For k = 0 To 15

                    temp = src((i * width * 4 + (k \ 4) * width + j * 4 + (k Mod 4)) * 4)
                    src((i * width * 4 + (k \ 4) * width + j * 4 + (k Mod 4)) * 4) = src((i * width * 4 + (k \ 4) * width + j * 4 + (k Mod 4)) * 4 + 2)
                    src((i * width * 4 + (k \ 4) * width + j * 4 + (k Mod 4)) * 4 + 2) = temp

                Next

            Next

        Next


        'Dim color(63) As Byte
        'Dim res(width * height - 1 + &H38) As Byte
        Dim res2(width * height - 1) As Byte
        res2 = ManagedSquish.SquishWrapper.CompressImage(src, width, height, ManagedSquish.SquishFlags.Dxt5)

        Array.ConstrainedCopy(res2, 0, resbyte, &H38, res2.Length)

        Select Case bmpnum

            Case 1

                Array.ConstrainedCopy(gtg256, 0, resbyte, 0, &H38)

            Case 2

                Array.ConstrainedCopy(gtg512, 0, resbyte, 0, &H38)

            Case 3

                Array.ConstrainedCopy(gtg192, 0, resbyte, 0, &H38)

            Case 4

                Array.ConstrainedCopy(gtg128, 0, resbyte, 0, &H38)

            Case 5

                Array.ConstrainedCopy(gtg64, 0, resbyte, 0, &H38)

        End Select

        LWMemoryEncode(resbyte, resbyte.Length, rscptr, olen, msg, Me.Handle)

        Dim reslw(olen - 1) As Byte

        Marshal.Copy(rscptr, reslw, 0, olen)

        Return reslw

    End Function

    Private Sub 불러오기ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 불러오기ToolStripMenuItem.Click

        Dim ofd1 As OpenFileDialog = New OpenFileDialog()
        Dim bm As Bitmap
        Dim gendir As String

        If ofd1.ShowDialog() = DialogResult.OK Then

            gendir = ofd1.FileName

            CheckBox1.Checked = False

        Else

            Exit Sub

        End If

        Dim bmp As Bitmap = Bitmap.FromFile(gendir)
        Dim destrect As Rectangle = New Rectangle(0, 0, 422, 600)

        With R

            If bmp.Width = bmp.Height Then

                .X = 0
                .Y = 0
                .Width = bmp.Width * (360 / 512)
                .Height = bmp.Height

            Else

                .X = 0
                .Y = 0
                .Width = bmp.Width
                .Height = bmp.Height

            End If


        End With

        If bmp.PixelFormat = PixelFormat.Format32bppArgb Or _
            bmp.PixelFormat = PixelFormat.Format32bppPArgb Or _
            bmp.PixelFormat = PixelFormat.Format32bppRgb Then

            bitm(1) = draw32(bmp, 2, R, destrect)
            PictureBox2.Image = bitm(1)

            'bitm(1).Save("bitm1.bmp", ImageFormat.Bmp)

        Else

            bm = draw32(bmp, 2, R, destrect)
            bitm(1) = bm.Clone(New Rectangle(0, 0, bm.Width, bm.Height), PixelFormat.Format32bppArgb)
            PictureBox2.Image = bitm(1)

        End If


        For i = 0 To 4

            If i = 1 Then

                imgcheck(i) = 1

            Else

                imgcheck(i) = 0

            End If

        Next

        'dest.Save("result4.bmp", ImageFormat.Bmp)

    End Sub

    Private Sub 불러오기ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles 불러오기ToolStripMenuItem1.Click

        Dim ofd1 As OpenFileDialog = New OpenFileDialog()
        Dim bm As Bitmap
        Dim gendir As String

        If ofd1.ShowDialog() = DialogResult.OK Then

            gendir = ofd1.FileName

            CheckBox1.Checked = False

        Else

            Exit Sub

        End If

        Dim bmp As Bitmap = Bitmap.FromFile(gendir)
        Dim destrect As Rectangle = New Rectangle(0, 0, 240, 240)

        With R

            .X = 0
            .Y = 0
            .Width = bmp.Width
            .Height = bmp.Height

        End With

        If bmp.PixelFormat = PixelFormat.Format32bppArgb Then

            bitm(0) = draw32(bmp, 1, R, destrect)
            PictureBox2.Image = bitm(0)

        Else

            bm = draw32(bmp, 1, R, destrect)
            bitm(0) = bm.Clone(New Rectangle(0, 0, bm.Width, bm.Height), PixelFormat.Format32bppArgb)
            PictureBox1.Image = bitm(0)

        End If




        For i = 0 To 4

            If i = 0 Then

                imgcheck(i) = 1

            Else

                imgcheck(i) = 0

            End If

        Next

        'dest.Save("result4.bmp", ImageFormat.Bmp)

    End Sub

    Dim R As New Rectangle()
    Dim ratio As Single
    Dim G As Graphics
    Dim pnum As Byte
    Dim destnum As Byte

    Private Sub cut(ByVal pn As Integer, ByVal destn As Integer)

        pnum = pn
        destnum = destn

        Select Case pnum

            Case 1

                G = PictureBox1.CreateGraphics()

            Case 2

                G = PictureBox2.CreateGraphics()

        End Select

        Select Case destnum

            Case 1

                R.X = 16
                R.Y = 30
                R.Width = 380
                R.Height = 380
                ratio = 1

            Case 3

                Select Case pnum

                    Case 2

                        R.X = 50
                        R.Y = 50
                        R.Width = 306
                        R.Height = 180
                        ratio = 204 / 120

                    Case 1

                        R.X = 20
                        R.Y = 20
                        R.Width = 204
                        R.Height = 120
                        ratio = 204 / 120

                End Select

            Case 4

                Select Case pnum

                    Case 2

                        R.X = 110
                        R.Y = 50
                        R.Width = 192
                        R.Height = 164
                        ratio = 96 / 82

                    Case 1

                        R.X = 50
                        R.Y = 30
                        R.Width = 144
                        R.Height = 123
                        ratio = 96 / 82

                End Select

            Case 5

                Select Case pnum

                    Case 2

                        R.X = 150
                        R.Y = 100
                        R.Width = 140
                        R.Height = 140
                        ratio = 1

                    Case 1

                        R.X = 80
                        R.Y = 50
                        R.Width = 100
                        R.Height = 100
                        ratio = 1

                End Select

        End Select

    End Sub

    Private Sub rectdraw()

        Select Case pnum

            Case 2

                PictureBox2.Refresh()

            Case 1

                PictureBox1.Refresh()

        End Select

        G.DrawRectangle(Pens.Cyan, R)

    End Sub

    'nobu33: 256x256 240x240
    'nobu34: 512x1024 422x600
    'nobu35: 256x128 204x120
    'nobu36: 128x128 96x82
    'nobu37: 64x64 48x48

    Private Sub PictureBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PictureBox2.KeyPress

        keyin(2, e.KeyChar)

    End Sub

    Private Sub PictureBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PictureBox1.KeyPress

        keyin(1, e.KeyChar)

    End Sub

    Private Sub keyin(ByVal pnum As Integer, ByVal ch As Char)

        Select Case ch

            Case "a"

                R.X -= 1
                rectdraw()

            Case "d"

                R.X += 1
                rectdraw()

            Case "w"

                R.Y -= 1
                rectdraw()

            Case "s"

                R.Y += 1
                rectdraw()

            Case "+"

                R.Width += (1 * ratio)
                R.Height += 1
                rectdraw()

            Case "-"

                R.Width -= (1 * ratio)
                R.Height -= 1
                rectdraw()

            Case Chr(Keys.Enter)

                cutimage()

        End Select

    End Sub

    Private Sub cutimage()

        Dim destrect As Rectangle

        Select Case destnum

            Case 1

                destrect = New Rectangle(0, 0, 240, 240)

                bitm(0) = draw32(PictureBox2.Image, 1, R, destrect)

                PictureBox1.Image = bitm(0)

                imgcheck(0) = 1

            Case 3

                destrect = New Rectangle(0, 0, 204, 120)

                Select Case pnum

                    Case 2

                        bitm(2) = draw32(PictureBox2.Image, 3, R, destrect)
                        PictureBox3.Image = bitm(2)
                        imgcheck(2) = 1

                    Case 1

                        bitm(2) = draw32(PictureBox1.Image, 3, R, destrect)
                        PictureBox3.Image = bitm(2)
                        imgcheck(2) = 1

                End Select

            Case 4

                destrect = New Rectangle(0, 0, 96, 82)

                Select Case pnum

                    Case 2

                        bitm(3) = draw32(PictureBox2.Image, 4, R, destrect)
                        PictureBox4.Image = bitm(3)
                        imgcheck(3) = 1

                    Case 1

                        bitm(3) = draw32(PictureBox1.Image, 4, R, destrect)
                        PictureBox4.Image = bitm(3)
                        imgcheck(3) = 1

                End Select

            Case 5

                destrect = New Rectangle(0, 0, 48, 48)

                Select Case pnum

                    Case 2

                        bitm(4) = draw32(PictureBox2.Image, 5, R, destrect)
                        PictureBox5.Image = bitm(4)
                        imgcheck(4) = 1

                    Case 1

                        bitm(4) = draw32(PictureBox1.Image, 5, R, destrect)
                        PictureBox5.Image = bitm(4)
                        imgcheck(4) = 1

                End Select

        End Select

    End Sub
    Private Function draw32(ByVal bmp As Bitmap, ByVal destnum As Integer, ByVal srcrect As Rectangle, ByVal destrect As Rectangle) As Bitmap

        Dim srcbyte() As Byte = bmp2bytes(bmp)
        Dim srcstr As MemoryStream = New MemoryStream(srcbyte)
        Dim src As Bitmap = Bitmap.FromStream(srcstr)
        Dim t As Byte

        Dim wid, hei As Integer

        Select Case destnum

            Case 1

                wid = 256
                hei = 256

            Case 2

                wid = 512
                hei = 1024

            Case 3

                wid = 256
                hei = 128

            Case 4

                wid = 128
                hei = 128

            Case 5

                wid = 64
                hei = 64

        End Select

        Dim dest As Bitmap = New Bitmap(wid, hei, PixelFormat.Format32bppArgb)

        Dim resize As Graphics = Graphics.FromImage(dest)

        resize = Graphics.FromImage(dest)

        With resize

            .DrawImage(src, destrect, srcrect.X, srcrect.Y, srcrect.Width, srcrect.Height, GraphicsUnit.Pixel)

        End With

        If destnum <> 3 And destnum <> 5 Then

            Dim destbyte((dest.Width * dest.Height) * 4 - 1 + &H36) As Byte
            Dim deststr As MemoryStream = New MemoryStream(destbyte)
            dest.Save(deststr, ImageFormat.Bmp)

            For i = 0 To src.Width - 1

                For j = 0 To src.Height - 1

                    t = srcbyte((j * src.Width + i) * 4 + 3 + &H36)
                    srcbyte((j * src.Width + i) * 4 + 3 + &H36) = srcbyte((j * src.Width + i) * 4 + &H36)
                    srcbyte((j * src.Width + i) * 4 + &H36) = t

                Next

            Next

            src = Bitmap.FromStream(srcstr)

            With resize

                .DrawImage(src, destrect, srcrect.X, srcrect.Y, srcrect.Width, srcrect.Height, GraphicsUnit.Pixel)

            End With

            For i = 0 To dest.Width - 1

                For j = 0 To dest.Height - 1

                    destbyte((j * dest.Width + i) * 4 + 3 + &H36) = dest.GetPixel(i, dest.Height - 1 - j).B

                Next

            Next

            dest = Bitmap.FromStream(deststr)

        End If
       
        Return dest

    End Function

    Private Function bmp2bytes(ByVal bmp As Bitmap) As Byte()

        bmp.RotateFlip(RotateFlipType.RotateNoneFlipY)
        Dim srcbyte(bmp.Width * bmp.Height * 4 - 1 + &H36) As Byte
        'Dim srcstr As MemoryStream = New MemoryStream(srcbyte)

        'bmp.Save(srcstr, ImageFormat.Bmp)

        Array.ConstrainedCopy(header512, 0, srcbyte, 0, &H36)
        Array.ConstrainedCopy(BitConverter.GetBytes(CULng(bmp.Width)), 0, srcbyte, &H12, 4)
        Array.ConstrainedCopy(BitConverter.GetBytes(CULng(bmp.Height)), 0, srcbyte, &H16, 4)
        Array.ConstrainedCopy(BmpToBytes(bmp), 0, srcbyte, &H36, srcbyte.Length - &H36)

        bmp.RotateFlip(RotateFlipType.RotateNoneFlipY)
        Return srcbyte

    End Function

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged

        If RadioButton1.Checked = True Then

            pnum = 2

            PictureBox2.Refresh()

            RadioButton2.Checked = False

        Else

            pnum = 1

            PictureBox1.Refresh()

            RadioButton2.Checked = True

        End If

    End Sub

    Private Sub 창조경로재설정ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 창조경로재설정ToolStripMenuItem.Click

        folder_select()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        If writegen(kaon) Then

            ListView1.LargeImageList.Images.Item(kaon Mod 100) = PictureBox5.Image

            ListView1.Refresh()

        End If

        ListView1.Focus()
        ListView1.Select()

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        Dim buffer(3) As Byte

        resfile.Position = &H110

        For i = 0 To 16

            buffer = BitConverter.GetBytes(recpos(i, 0))
            resfile.Write(buffer, 0, 4)

            buffer = BitConverter.GetBytes(recpos(i, 1))
            resfile.Write(buffer, 0, 4)

        Next

        resfile.SetLength(resfile.Length + sumdiff)

        For i = 0 To 5

            resfile.Position = recpos(i, 0)
            resfile.Write(res(i), 0, res(i).Length)
            resfile.Flush()

        Next

        MsgBox("저장되었습니다.")
        sumdiff = 0

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

        If pnum = 1 Then

            Exit Sub

        End If

        destnum = 1

        cut(pnum, 1)

        If pnum = 2 Then

            PictureBox2.Focus()

        End If

        rectdraw()

    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click

        destnum = 3

        cut(pnum, 3)

        Select Case pnum

            Case 2

                PictureBox2.Focus()

            Case 1

                PictureBox1.Focus()

        End Select

        rectdraw()

    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click

        destnum = 4

        cut(pnum, 4)

        Select Case pnum

            Case 2

                PictureBox2.Focus()

            Case 1

                PictureBox1.Focus()

        End Select

        rectdraw()


    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click

        destnum = 5

        cut(pnum, 5)

        Select Case pnum

            Case 2

                PictureBox2.Focus()

            Case 1

                PictureBox1.Focus()

        End Select

        rectdraw()

    End Sub

    'nobu33: 256x256 240x240
    'nobu34: 512x1024 422x600
    'nobu35: 256x128 204x120
    'nobu36: 128x128 96x82
    'nobu37: 64x64 48x48

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        Dim alpha As Byte
        Dim bmparray() As Byte

        If CheckBox1.Checked = True Then

            For i = 0 To 4

                If imgcheck(i) = 1 Then

                    Select Case i + 1

                        Case 1

                            bmparray = bmp2bytes(PictureBox1.Image)

                            For j = 0 To 255

                                For k = 0 To 255

                                    alpha = bmparray(j * 256 * 4 + k * 4 + &H36 + 3)

                                    For l = 0 To 2

                                        bmparray(j * 256 * 4 + k * 4 + &H36 + l) = CInt(bmparray(j * 256 * 4 + k * 4 + &H36 + l)) * alpha \ &HFF

                                    Next

                                Next

                            Next

                            Dim memstr As MemoryStream = New MemoryStream(bmparray)
                            PictureBox1.Image = Bitmap.FromStream(memstr)
                            memstr.Dispose()

                        Case 2

                            bmparray = bmp2bytes(PictureBox2.Image)

                            For j = 0 To 511

                                For k = 0 To 1023

                                    alpha = bmparray(j * 1024 * 4 + k * 4 + &H36 + 3)

                                    For l = 0 To 2

                                        bmparray(j * 1024 * 4 + k * 4 + &H36 + l) = CInt(bmparray(j * 1024 * 4 + k * 4 + &H36 + l)) * alpha \ &HFF

                                    Next

                                Next

                            Next

                            Dim memstr As MemoryStream = New MemoryStream(bmparray)
                            PictureBox2.Image = Bitmap.FromStream(memstr)
                            memstr.Dispose()

                        Case 3

                            bmparray = bmp2bytes(PictureBox3.Image)

                            For j = 0 To 255

                                For k = 0 To 127

                                    alpha = bmparray(j * 128 * 4 + k * 4 + &H36 + 3)

                                    For l = 0 To 2

                                        bmparray(j * 128 * 4 + k * 4 + &H36 + l) = CInt(bmparray(j * 128 * 4 + k * 4 + &H36 + l)) * alpha \ &HFF

                                    Next

                                Next

                            Next

                            Dim memstr As MemoryStream = New MemoryStream(bmparray)
                            PictureBox3.Image = Bitmap.FromStream(memstr)
                            memstr.Dispose()

                        Case 4

                            bmparray = bmp2bytes(PictureBox4.Image)

                            For j = 0 To 127

                                For k = 0 To 127

                                    alpha = bmparray(j * 128 * 4 + k * 4 + &H36 + 3)

                                    For l = 0 To 2

                                        bmparray(j * 128 * 4 + k * 4 + &H36 + l) = CInt(bmparray(j * 128 * 4 + k * 4 + &H36 + l)) * alpha \ &HFF

                                    Next

                                Next

                            Next

                            Dim memstr As MemoryStream = New MemoryStream(bmparray)
                            PictureBox4.Image = Bitmap.FromStream(memstr)
                            memstr.Dispose()

                        Case 5

                            bmparray = bmp2bytes(PictureBox5.Image)

                            For j = 0 To 63

                                For k = 0 To 63

                                    alpha = bmparray(j * 64 * 4 + k * 4 + &H36 + 3)

                                    For l = 0 To 2

                                        bmparray(j * 64 * 4 + k * 4 + &H36 + l) = CInt(bmparray(j * 64 * 4 + k * 4 + &H36 + l)) * alpha \ &HFF

                                    Next

                                Next

                            Next

                            Dim memstr As MemoryStream = New MemoryStream(bmparray)
                            PictureBox5.Image = Bitmap.FromStream(memstr)
                            memstr.Dispose()

                    End Select

                End If

            Next

        Else

            For i = 0 To 4

                If imgcheck(i) = 1 Then

                    Select Case i + 1

                        Case 1

                            PictureBox1.Image = bitm(0)

                        Case 2

                            PictureBox2.Image = bitm(1)

                        Case 3

                            PictureBox3.Image = bitm(2)

                        Case 4

                            PictureBox4.Image = bitm(3)

                        Case 5

                            PictureBox5.Image = bitm(4)

                    End Select

                End If

            Next

        End If

    End Sub

    Dim sp, ep As Point
    Dim isdrag As Boolean = False

    Private Sub PictureBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseClick

        If RadioButton2.Checked = True Then

            If e.Button = Windows.Forms.MouseButtons.Right Then

                cutimage()

            End If

        End If

    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown

        If RadioButton2.Checked = True Then

            sp = New Point(e.X, e.Y)

            isdrag = True

        End If

    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove

        If RadioButton2.Checked = True Then

            If isdrag = True Then

                ep = New Point(e.X, e.Y)

                R.X += ((ep.X - sp.X))
                R.Y += ((ep.Y - sp.Y))

                sp = ep

                rectdraw()

            End If

        End If

    End Sub

    Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp

        If RadioButton2.Checked = True Then

            isdrag = False

        End If

    End Sub

    Private Sub PictureBox1_MouseWheel(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseWheel

        If RadioButton2.Checked = True Then

            R.Width += ((e.Delta / 10) * ratio)
            R.Height += (e.Delta / 10)
            rectdraw()

        End If

    End Sub

    Private Sub PictureBox2_MouseClick(sender As Object, e As MouseEventArgs) Handles PictureBox2.MouseClick

        If RadioButton1.Checked = True Then

            If e.Button = Windows.Forms.MouseButtons.Right Then

                cutimage()

            End If

        End If

    End Sub

    Private Sub PictureBox2_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox2.MouseDown

        If RadioButton1.Checked = True Then

            sp = New Point(e.X, e.Y)

            isdrag = True

        End If

    End Sub

    Private Sub PictureBox2_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox2.MouseMove

        If RadioButton1.Checked = True Then

            If isdrag = True Then

                ep = New Point(e.X, e.Y)

                R.X += ((ep.X - sp.X))
                R.Y += ((ep.Y - sp.Y))

                sp = ep

                rectdraw()

            End If

        End If

    End Sub

    Private Sub PictureBox2_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox2.MouseUp

        If RadioButton1.Checked = True Then

            isdrag = False

        End If

    End Sub

    Private Sub PictureBox2_MouseWheel(sender As Object, e As MouseEventArgs) Handles PictureBox2.MouseWheel

        If RadioButton1.Checked = True Then

            R.Width += ((e.Delta / 10) * ratio)
            R.Height += (e.Delta / 10)
            rectdraw()

        End If

    End Sub

End Class
