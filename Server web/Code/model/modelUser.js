const mongoose = require('mongoose');
var schema = new mongoose.Schema({
    name : String,
    user : String,
    enableAll: Boolean, 
    enableCurrent: Boolean,
    enableFollowing: Boolean,
},
    {collection : "settings"});
module.exports = mongoose.model('modelUser', schema);