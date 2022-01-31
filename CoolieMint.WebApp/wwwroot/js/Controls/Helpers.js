function drawCircle(element, percent, text) {
    if (percent < 0 || percent > 100) {
        return;
    }

    clearCanvas(element);
    var activeColor = "#FFFFFF";
    var inactiveColor = "#535353";
    drawCircleFromStart(element, percent, activeColor);
    drawCircleFromEnd(element, 100 - percent, inactiveColor);
    drawCircleText(element, text);
}

function drawCircleFromStart(element, percent, color) {
    var ctx = $(element)[0].getContext("2d");

    var startPoint = 0.75 * Math.PI;
    var stepLength = 1.5 * Math.PI * (percent / 100.0);
    var actualEndpoint = stepLength + startPoint;
    if (actualEndpoint > Math.PI * 2) {
        actualEndpoint -= Math.PI * 2;
    }

    ctx.beginPath();
    ctx.lineWidth = 7;
    ctx.strokeStyle = color;
    ctx.arc(61, 70, 50, startPoint, actualEndpoint);

    ctx.stroke();
}

function drawCircleFromEnd(element, percent, color) {
    var ctx = $(element)[0].getContext("2d");

    var endPoint = 0.25 * Math.PI;
    var stepLength = 1.5 * Math.PI * (percent / 100.0);
    var actualStartPoint = endPoint - stepLength;
    if (actualStartPoint < 0) {
        actualStartPoint = Math.PI * 2 + actualStartPoint;
    }

    ctx.beginPath();
    ctx.lineWidth = 7;
    ctx.strokeStyle = color;
    ctx.arc(61, 70, 50, actualStartPoint, endPoint);

    ctx.stroke();
}

function drawCircleText(element, text) {
    var ctx = $(element)[0].getContext("2d");
    ctx.font = "25px Arial";
    ctx.lineWidth = 4;
    ctx.fillStyle = "#FFFFFF";
    ctx.textAlign = "center";
    ctx.fillText(text , 63, 75);
}

function drawCircleSmallText(element, text) {
    var context = $(element)[0].getContext("2d");
    context.font = "15px Arial"; 
    context.lineWidth = 3; 
    context.fillStyle = "#FFFFFF"; 
    context.textAlign = "center"; 
    context.fillText(text, 63, 95);
}

function clearCanvas(element) {
    var canvas = $(element)[0];
    var ctx = canvas.getContext("2d");
    ctx.clearRect(0, 0, canvas.width, canvas.height);
}