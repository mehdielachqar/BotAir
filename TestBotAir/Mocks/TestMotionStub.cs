using System;
using Xunit;

namespace TestBotAir.Mocks
{
    public class TestMotionStub
    {
        [Fact]
        public void InitializationWithInvalidPattern_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MotionStub("* ?*\n "));
        }
        [Fact]
        public void InitializationWithNoInitialPosition_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new MotionStub("* *"));
        }
        [Fact]
        public void InitializationWithTooManyPosition_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new MotionStub("* > \n > *"));
        }
        [Fact]
        public void InitializationWithNonRectangle_FillWithEmpty()
        {
            var test = new MotionStub(">\n**");
            Assert.True(test.Move(1));
        }
        [Fact]
        public void MoveOutHorizontaly_ThrowsInvalidOperation()
        {
            var test = new MotionStub(">\n ");
            Assert.Throws<InvalidOperationException>(()=>test.Move(1));
        }
        [Fact]
        public void MoveOutVerticaly_ThrowsInvalidOperation()
        {
            var test = new MotionStub("> ");
            test.Rotate(90);
            Assert.Throws<InvalidOperationException>(() => test.Move(1));
        }
        [Fact]
        public void MoveWithBadDistance_ThrowsArgumentOutOfRange()
        {
            var test = new MotionStub(">");

            Assert.Throws<ArgumentOutOfRangeException>(()=>test.Move(0));
        }
        [Fact]
        public void MoveWithNoObstacle_ReturnTrue()
        {
            var test = new MotionStub("> *");

            Assert.True(test.Move(1));
        }

        [Fact]
        public void MoveWithObstacle_ReturnFalse()
        {
            var test = new MotionStub(">*");

            Assert.False(test.Move(1));
        }
        [Fact]
        public void MoveWithObstacle_DoNotMove()
        {
            var test = new MotionStub(">* ");

            test.Move(1);
            Assert.False(test.Move(1));
        }
        [Fact]
        public void Move3WithObstacle_ReturnFalse()
        {
            var test = new MotionStub("> * ");

            Assert.False(test.Move(3));
        }
        [Fact]
        public void Move3WithObstacle_DoNotMove()
        {
            var test = new MotionStub("> * **");

            test.Move(3);
            Assert.True(test.Move(1));
        }
        [Fact]
        public void Move1WithNoObstacle_StepsThroughTheMap()
        {
            var test = new MotionStub("> *");

            test.Move(1);
            Assert.False(test.Move(1));
        }
        [Fact]
        public void RotateWithNotRightAngles_ThrowsNotSupported()
        {
            var test = new MotionStub(">");

            Assert.Throws<NotSupportedException>(() => test.Rotate( 30));
            Assert.Throws<NotSupportedException>(() => test.Rotate(-30));
        }
        [Fact]
        public void RotateWithBigAngles_ThrowsNotSupported()
        {
            var test = new MotionStub(">");

            Assert.Throws<NotSupportedException>(() => test.Rotate( 360));
            Assert.Throws<NotSupportedException>(() => test.Rotate(-360));
        }
        [Fact]
        public void Rotate90_ChangeDirectionClockwise()
        {
            var test = new MotionStub(
                "*>*\n"+
                "* *"
            );
            test.Rotate(90);
            Assert.True(test.Move(1));
        }
        [Fact]
        public void RotateMultipleTimes90_ChangeDirectionClockwise()
        {
            var test = new MotionStub(
                "*> *\n" +
                "****"
            );
            test.Rotate(180);
            test.Rotate(90);
            test.Rotate(90);
            Assert.True(test.Move(1));
        }
        [Fact]
        public void Rotate180_InvertDirection()
        {
            var test = new MotionStub(" >*");
            test.Rotate(180);
            Assert.True(test.Move(1));
        }
        [Fact]
        public void Rotate270_InvertDirection()
        {
            var test = new MotionStub(
                "* *\n"+
                "*>*"
            );
            test.Rotate(270);
            Assert.True(test.Move(1));
        }
        [Fact]
        public void RotateNeg90_ChangeDirectionCounterClockwise()
        {
            var test = new MotionStub(
                "* *\n" +
                "*>*"
            );
            test.Rotate(-90);
            Assert.True(test.Move(1));
        }
    }
}
