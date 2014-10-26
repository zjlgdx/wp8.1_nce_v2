
1, http://m.hujiang.com/en_nce/
http://m.hujiang.com/handler/appweb.json?v=0.6686999415978789&op=GetBookList&key=nce&callback=jsonp1

jsonp1({"Code":0,"Message":"","Value":[{"Key":"xingainian1","BookIndex":"1","EnName":"New Concept English The First Stage","CnName":"新概念英语第一册","ShortName":"第一册"},{"Key":"xingainian2","BookIndex":"2","EnName":"New Concept English The Second Stage","CnName":"新概念英语第二册","ShortName":"第二册"},{"Key":"xingainian3","BookIndex":"3","EnName":"New Concept English The Third Stage","CnName":"新概念英语第三册","ShortName":"第三册"},{"Key":"xingainian4","BookIndex":"4","EnName":"New Concept English The Fourth Stage","CnName":"新概念英语第四册","ShortName":"第四册"}]})

json: GetBookList.json

2, http://m.hujiang.com/en_nce/xingainian1/
2.1 http://m.hujiang.com/handler/appweb.json?v=0.3706367036793381&op=GetBook&Key=nce&bookKey=xingainian1&callback=jsonp2

jsonp2({"Code":0,"Message":"","Value":{"Key":"xingainian1","BookIndex":"1","EnName":"New Concept English The First Stage","CnName":"新概念英语第一册","ShortName":"第一册"}})

json: GetBook.json

2.2 http://m.hujiang.com/handler/appweb.json?v=0.9860641923733056&op=GetBookUnitList&key=nce&bookKey=xingainian1&callback=jsonp1

jsonp1({"Code":0,"Message":"","Value":[{"Key":"1-1-24","UnitIndex":"1-24","Name":"新概念英语第一册(1-24课)"},{"Key":"1-25-48","UnitIndex":"25-48","Name":"新概念英语第一册(25-48课)"},{"Key":"1-49-72","UnitIndex":"49-72","Name":"新概念英语第一册(49-72课)"},{"Key":"1-73-96","UnitIndex":"73-96","Name":"新概念英语第一册(73-96课)"},{"Key":"1-97-120","UnitIndex":"97-120","Name":"新概念英语第一册(97-120课)"},{"Key":"1-121-144","UnitIndex":"121-144","Name":"新概念英语第一册(121-144课)"}]})

json: GetBookUnitList.json

3, http://m.hujiang.com/en_nce/xingainian1/1-1-24/
3.1 http://m.hujiang.com/handler/appweb.json?v=0.7858805949799716&op=GetBookUnit&key=nce&unitKey=1-1-24&callback=jsonp2

jsonp2({"Code":0,"Message":"","Value":{"Key":"1-1-24","UnitIndex":"1-24","Name":"新概念英语第一册(1-24课)"}})

json: GetBookUnit.json

3.2 http://m.hujiang.com/handler/appweb.json?v=0.19058830104768276&op=GetBookTextList&key=nce&unitKey=1-1-24&callback=jsonp1

jsonp1({"Code":0,"Message":"","Value":[{"Key":"1-1-24-1","Index":"1-2","Name":"Excuse me!","Video":"http://bm1.33.play.bokecc.com/flvs/ca/Qxti6/uFfe1ymziK-10.mp4?t=1399130261\u0026key=97815461F06EC195F2C046259C70310C"},{"Key":"1-1-24-2","Index":"3-4","Name":"Sorry, sir.","Video":"http://bm1.33.play.bokecc.com/flvs/ca/Qxtgv/uOJ4tMLh7u-10.mp4?t=1399130174\u0026key=D27F54C5A0C53E2D66DEEDFC65AC8663"},{"Key":"1-1-24-3","Index":"5-6","Name":"Nice to meet you","Video":"http://bm1.33.play.bokecc.com/flvs/ca/Qxtgv/uN43oBe6z4-10.mp4?t=1399131507\u0026key=43FF43B94630F2971A8AA2655F71DBCF"},{"Key":"1-1-24-4","Index":"7-8","Name":" Are you a teacher?","Video":""},{"Key":"1-1-24-5","Index":"9-10","Name":"How are you today?","Video":""},{"Key":"1-1-24-6","Index":"11-12","Name":" Is this your shirt?","Video":""},{"Key":"1-1-24-7","Index":"13-14","Name":" A new dress","Video":""},{"Key":"1-1-24-8","Index":"15-16","Name":"Your passports, please.","Video":""},{"Key":"1-1-24-9","Index":"17-18","Name":"How do you do?","Video":""},{"Key":"1-1-24-10","Index":"19-20","Name":"Tired and thirsty","Video":""},{"Key":"1-1-24-11","Index":"21-22","Name":"Which book?","Video":""},{"Key":"1-1-24-12","Index":"23-24","Name":"Which glasses?","Video":""}]})

json: GetBookTextList.json

4, http://m.hujiang.com/en_nce/xingainian1/1-1-24-1/

4.1 http://m.hujiang.com/handler/appweb.json?v=0.9384690483566374&op=GetBookText&key=nce&textKey=1-1-24-1&callback=jsonp6

jsonp6({"Code":0,"Message":"","Value":{"Key":"1-1-24-1","Index":"1-2","Name":"Excuse me!","Video":"http://bm1.33.play.bokecc.com/flvs/ca/Qxti6/uFfe1ymziK-10.mp4?t=1399130261\u0026key=97815461F06EC195F2C046259C70310C"}})

json: GetBookText.json

4.2 http://m.hujiang.com/handler/appweb.json?v=0.8839236546773463&op=GetXiangJieList&bookIndex=1&startIndex=1&endIndex=24&currentIndex=1&callback=jsonp5

jsonp5({"Code":0,"Message":"","Value":[{"LineContent":"课文详注 Further notes on the text"},{"LineContent":"1．Excuse me 对不起。"},{"LineContent":"这是常用于表示道歉的客套话，相当于汉语中的“劳驾”、“对不起”。当我们要引起别人的注意、要打搅别人或打断别人的话时，通常都可使用这一表达方式。在课文中，男士为了吸引女士的注意而使用了这句客套话。它也可用在下列场合：向陌生人问路，借用他人的电话，从别人身边挤过，在宴席或会议中途要离开一会儿等等。"},{"LineContent":"2．Yes？什么事？"},{"LineContent":"课文中的 Yes？应用升调朗读，意为：“什么事？”Yes？以升调表示某种不肯定或询问之意，也含有请对方说下去的意思。"},{"LineContent":"3．Pardon？对不起，请再说一遍。"},{"LineContent":"当我们没听清或没理解对方的话并希望对方能重复一遍时，就可以使用这一表达方式。较为正式的说法是："},{"LineContent":"I beg your pardon.  I beg your pardon?   Pardon me."},{"LineContent":"它们在汉语中的意思相当于“对不起，请再说一遍”或者“对不起，请再说一遍好吗？”"},{"LineContent":"4．Thank you very much．非常感谢！"},{"LineContent":"这是一句表示感谢的用语，意为“非常感谢（你）”。请看下列类似的表达式，并注意其语气上的差异："},{"LineContent":"Thank you.  谢谢（你）。   Thanks!  谢谢！"},{"LineContent":"5．数字1～10的英文写法"},{"LineContent":"1—one     2—two      3—three      4—four      5—five"},{"LineContent":"6—six      7—seven     8—eight      9—nine    10—ten"},{"LineContent":"语法 Grammar in use"},{"LineContent":"一般疑问句"},{"LineContent":"一般疑问句根据其结构又分为若干种。通过主谓倒装可将带有be的陈述句变为一般疑问句。即将be的适当形式移到主语之前，如："},{"LineContent":"陈述句：This is your watch.  这是你的手表。"},{"LineContent":"疑问句：Is this your watch?  这是你的手表吗？"},{"LineContent":"（可参见 Lessons 15～16语法部分有关 be的一般现在时形式的说明。）"},{"LineContent":"词汇学习  Word study"},{"LineContent":"1．coat  n.  上衣，外套：  Is this your coat?   这是你的外套吗？"},{"LineContent":"coat and skirt\u003c英\u003e（上衣、裙子匹配的）西式女套装"},{"LineContent":"2．dress  n."},{"LineContent":"（1）连衣裙；套裙：  Is this your dress?  这是你的连衣裙吗？"},{"LineContent":"（2）服装；衣服：  casual dress 便服； evening dress      晚礼服"}]})

json: GetXiangJieList.json

4.3 http://m.hujiang.com/handler/appweb.json?v=0.8261403972283006&op=GetCiHuiList&bookIndex=1&startIndex=1&endIndex=24&currentIndex=1&callback=jsonp4

jsonp4({"Code":0,"Message":"","Value":[{"Word":"excuse","Pronounce":"iks’kju:z, iks’kju:s","Comment":"v.原谅"},{"Word":"me","Pronounce":"mi:, mi","Comment":"pron.我(宾格)"},{"Word":"yes","Pronounce":"jes","Comment":"ad.是的"},{"Word":"is","Pronounce":"iz","Comment":"v.be动词现在时第三人称单数"},{"Word":"this","Pronounce":"ðis","Comment":"pron.这"},{"Word":"your","Pronounce":"jɔ:","Comment":"pron.你的，你们的"},{"Word":"handbag","Pronounce":"‘hændbæɡ","Comment":"n.(女用)手提包"},{"Word":"pardon","Pronounce":"‘pa:dn","Comment":"int.原谅，请再说一遍"},{"Word":"it","Pronounce":"it","Comment":"pron.它"},{"Word":"thank you","Pronounce":"’θæŋk-ju:","Comment":"vt.感谢"},{"Word":"very much","Pronounce":"‘veri-mʌtʃ","Comment":"非常地"},{"Word":"pen","Pronounce":"pen","Comment":"n.钢笔"},{"Word":"pencil","Pronounce":"pensl","Comment":"n.铅笔"},{"Word":"book","Pronounce":"buk","Comment":"n.书"},{"Word":"watch","Pronounce":"wɔtʃ","Comment":"n.手表"},{"Word":"coat","Pronounce":"kəut","Comment":"n.上衣，外衣"},{"Word":"dress","Pronounce":"dres","Comment":"n.连衣裙"},{"Word":"skirt","Pronounce":"skə:t","Comment":"n.裙子"},{"Word":"shirt","Pronounce":"ʃə:t","Comment":"n.衬衣"},{"Word":"car","Pronounce":"kɑ:","Comment":"n.小汽车"},{"Word":"house","Pronounce":"haus","Comment":"n.房子"}]})

json: GetCiHuiList.json

4.4 http://m.hujiang.com/handler/appweb.json?v=0.6368643059395254&op=GetYuanWenList&bookIndex=1&startIndex=1&endIndex=24&currentIndex=1&callback=jsonp2

jsonp2({"Code":0,"Message":"","Value":[{"Time":"00:10.94","Sentence":"Whose handbag is it?"},{"Time":"00:15.24","Sentence":"Excuse me!"},{"Time":"00:16.83","Sentence":"Yes?"},{"Time":"00:18.33","Sentence":"Is this your handbag?"},{"Time":"00:21.47","Sentence":"Pardon?"},{"Time":"00:23.18","Sentence":"Is this your handbag?"},{"Time":"00:26.29","Sentence":"Yes it is."},{"Time":"00:30.34","Sentence":"Thank you very much."}]})

json: GetYuanWenList.json

4.5 http://m.hujiang.com/handler/appweb.json?v=0.29309563525021076&op=GetShuangYuList&bookIndex=1&startIndex=1&endIndex=24&currentIndex=1&callback=jsonp3

jsonp3({"Code":0,"Message":"","Value":[{"Time":"00:10.94","Sentence":"Whose handbag is it?","CnSentence":""},{"Time":"00:15.24","Sentence":"Excuse me!","CnSentence":"对不起。"},{"Time":"00:16.83","Sentence":"Yes?","CnSentence":"什么事？"},{"Time":"00:18.33","Sentence":"Is this your handbag?","CnSentence":"这是您的手提包吗？"},{"Time":"00:21.47","Sentence":"Pardon?","CnSentence":"对不起，请再说一遍。"},{"Time":"00:23.18","Sentence":"Is this your handbag?","CnSentence":"这是您的手提包吗？"},{"Time":"00:26.29","Sentence":"Yes it is.","CnSentence":"是的，是我的。"},{"Time":"00:30.34","Sentence":"Thank you very much.","CnSentence":"非常感谢！"}]})

json: GetShuangYuList.json

4.6 http://s.hujiang.com/outapi/webdata.ashx?v=0.0787900376599282&op=GetTopicsByMoreBoardIdList&curPage=1&pageSize=20&boardID=65&isHot=-1&isTop=-1&isBest=-1&orderType=2&callback=jsonp1

5 
http://f1.w.hjfile.cn/doc/touch_m/nce/3-41-50/am_yuanwen_42.mp3
http://f1.w.hjfile.cn/doc/touch_m/nce/3-41-50/yuanwen_42.mp3