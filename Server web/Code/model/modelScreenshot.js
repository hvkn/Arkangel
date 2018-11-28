const mongoose = require('mongoose');
var schema = new mongoose.Schema({
    name : String,
    user : String,
    enableScreenshot: Boolean,
    hoursScreenshot: Number,
    minutesScreenshot: Number,
    enableTimestamp: Boolean,
    enableDouble: Boolean,
    enableDelete: Boolean,
    deleteScreenshot: Number,
    qualityScreenshot: Number,
    dateDelete: String
},
    { collection: "settings" });
module.exports = mongoose.model('modelScreenshot', schema);