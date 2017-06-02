MarqueeTool = function (element) {
    return typeof (element) == 'object' ? element : document.getElementById(element);
},

RandStr = function (n, u) {
    var tmStr = "abcdefghijklmnopqrstuvwxyz0123456789";
    var Len = tmStr.length;
    var Str = "";
    for (i = 1; i < n + 1; i++) {
        Str += tmStr.charAt(Math.random() * Len);
    }
    return (u ? Str.toUpperCase() : Str);
};
var MyMarquees = new Array();

function getMyMQName(mName) {
    var name = mName == undefined ? RandStr(5) : mName;
    var myNames = ',' + MyMarquees.join(',') + ',';

    while (myNames.indexOf(',' + name + ',') != -1) {
        name = RandStr(5);
    }
    return name;
}

function Marquee(inits) {
    var _o = this;
    var _i = inits;

    if (_i.obj == undefined) return;
    _o.mode = _i.mode == undefined ? 'x' : _i.mode;
    _o.mName = getMyMQName(_i.name);       
    _o.mObj = MarqueeTool(_i.obj);         
    _o.speed = _i.speed == undefined ? 10 : _i.speed;
    _o.autoStart = _i.autoStart == undefined ? true : _i.autoStart;
    _o.movePause = _i.movePause == undefined ? true : _i.movePause;

    _o.mDo = null;           
    _o.pause = false;        

    _o.init = function () {
        //if ((_o.mObj.scrollWidth <= _o.mObj.offsetWidth && _o.mode == 'x') && (_o.mObj.scrollHeight <= _o.mObj.offsetHeight && _o.mode == 'y')) return;
        if ((_o.mObj.scrollWidth <= _o.mObj.offsetWidth && _o.mode == 'x') || (_o.mObj.scrollHeight <= _o.mObj.offsetHeight && _o.mode == 'y')) return;

        MyMarquees.push(_o.mName);

        _o.mObj.innerHTML = _o.mode == 'x' ? (
         '<table width="100%" border="0" align="left" cellpadding="0" cellspace="0">' +
         ' <tr>' +
         '  <td id="MYMQ_' + _o.mName + '_1">' + _o.mObj.innerHTML + '</td>' +
         '  <td id="MYMQ_' + _o.mName + '_2">' + _o.mObj.innerHTML + '</td>' +
         ' </tr>' +
         '</table>'
        ) : (
         '<div id="MYMQ_' + _o.mName + '_1">' + _o.mObj.innerHTML + '</div>' +
         '<div id="MYMQ_' + _o.mName + '_2">' + _o.mObj.innerHTML + '</div>'
        );

        _o.mObj1 = MarqueeTool('MYMQ_' + _o.mName + '_1');
        _o.mObj2 = MarqueeTool('MYMQ_' + _o.mName + '_2');
        _o.mo1Width = _o.mObj1.scrollWidth;
        _o.mo1Height = _o.mObj1.scrollHeight;

        if (_o.autoStart) _o.start();
    };

    //start
    _o.start = function () {
        _o.mDo = setInterval((_o.mode == 'x' ? _o.moveX : _o.moveY), _o.speed);
        if (_o.movePause) {
            _o.mObj.onmouseover = function () { _o.pause = true; }
            _o.mObj.onmouseout = function () { _o.pause = false; }
        }
    }

    //stop
    _o.stop = function () {
        clearInterval(_o.mDo)
        _o.mObj.onmouseover = function () { }
        _o.mObj.onmouseout = function () { }
    }

    // moveX
    _o.moveX = function () {
        if (_o.pause) return;
        var left = _o.mObj.scrollLeft;
        if (left == _o.mo1Width) {
            _o.mObj.scrollLeft = 0;
        } else if (left > _o.mo1Width) {
            _o.mObj.scrollLeft = left - _o.mo1Width;
        } else {
            _o.mObj.scrollLeft++;
        }
    };

    //moveY
    _o.moveY = function () {
        if (_o.pause) return;
        var top = _o.mObj.scrollTop;
        if (top == _o.mo1Height) {
            _o.mObj.scrollTop = 0;
        } else if (top > _o.mo1Height) {
            _o.mObj.scrollTop = top - _o.mo1Height;
        } else {
            _o.mObj.scrollTop++;
        }
    };

    _o.init();
}