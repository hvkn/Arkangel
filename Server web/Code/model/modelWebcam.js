const mongoose = require('mongoose');
var schema = new mongoose.Schema({
    name: String,
    user : String,
    enableWebcam: Boolean,
    hoursWebcam: Number,
    minutesWebcam: Number,
    enableDelete: Boolean,
    daysWebcam: Number,
    enableDeleteUpload: Boolean,
    dateDelete: String
},
    { collection: "settings" });
module.exports = mongoose.model('modelWebcam', schema);