using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace sidhe3141utils.Xna.GameObject
{
    /// <summary>
    /// An object in a 2D game.
    /// Author: James Yakura/sidhe3141
    /// </summary>
    public abstract class GameObject2D:GameObject
    {
        Rectangle drawRectangle;
        Texture2D sprite;
        int frames_per_row;
        int rows;

        //Reversion variables
        Rectangle revertDrawRectangle;

        /// <summary>
        /// Creates a new game object with a static animation.
        /// </summary>
        /// <param name="x">The game object's starting x-position.</param>
        /// <param name="y">The game object's starting y-position.</param>
        /// <param name="sprite">The game object's sprite.</param>
        public GameObject2D(int x, int y, Texture2D sprite):this(new Rectangle(x,y,sprite.Bounds.Width,sprite.Bounds.Height),sprite,1,1)
        {
        }
        /// <summary>
        /// Creates a new game object with a static animation and x and y scaling.
        /// </summary>
        /// <param name="drawRectangle">The starting draw rectangle.</param>
        /// <param name="sprite">The object's sprite.</param>
        public GameObject2D(Rectangle drawRectangle, Texture2D sprite):this(drawRectangle,sprite,1,1)
        {
        }
        /// <summary>
        /// Creates a new game object with an animation and x and y scaling.
        /// </summary>
        /// <param name="drawRectangle">The starting draw rectangle.</param>
        /// <param name="sprite">The sprite or sprite strip.</param>
        /// <param name="frames_per_row">The number of frames per row in the sprite strip.</param>
        /// <param name="rows">The number of rows in the sprite strip.</param>
        public GameObject2D(Rectangle drawRectangle, Texture2D sprite, int frames_per_row, int rows):base()
        {
            this.sprite = sprite;
            this.drawRectangle = drawRectangle;
            this.frames_per_row = frames_per_row;
            this.rows = rows;
        }

        /// <summary>
        /// Gets and sets the draw rectangle.
        /// </summary>
        public Rectangle DrawRectangle
        {
            get
            {
                return drawRectangle;
            }
            set
            {
                drawRectangle = value;
            }
        }
        /// <summary>
        /// Gets and sets the object's sprite. Note: This does not recalculate the target rectangle dimensions.
        /// </summary>
        public Texture2D Sprite
        {
            get
            {
                return sprite;
            }
            set
            {
                sprite = value;
            }
        }

        //public override void  Draw(SpriteBatch spriteBatch, Rectangle camera)
        //{
        //    Rectangle sourceRectangle = new Rectangle(
        //        (Frame % frames_per_row) * sprite.Bounds.Width/frames_per_row, //X: Frame mod frames-per-row, times width.
        //        Frame / frames_per_row * sprite.Bounds.Height/rows, //Frame over frames per row times width.
        //        sprite.Bounds.Width/frames_per_row,
        //        sprite.Bounds.Height/rows);
        //    Rectangle newDrawRectangle = 
        //        new Rectangle(drawRectangle.X - camera.X, drawRectangle.Y = camera.Y, drawRectangle.Width, drawRectangle.Height);
        //    spriteBatch.Draw(sprite, newDrawRectangle, sourceRectangle, this.Color);
        //}
        /// <summary>
        /// Moves the draw rectangle, transforming it rather than replacing it.
        /// </summary>
        /// <param name="location">The new draw rectangle location.</param>
        public virtual void MoveTo(Rectangle location)
        {
            drawRectangle.X = location.X;
            drawRectangle.Y = location.Y;
            drawRectangle.Width = location.Width;
            drawRectangle.Height = location.Height;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(
                (Frame % frames_per_row) * sprite.Bounds.Width / frames_per_row, //X: Frame mod frames-per-row, times width.
                Frame / frames_per_row * sprite.Bounds.Height / rows, //Frame over frames per row times width.
                sprite.Bounds.Width / frames_per_row,
                sprite.Bounds.Height / rows);
            spriteBatch.Draw(sprite, drawRectangle, sourceRectangle, Color);
        }
        public override void Snapshot()
        {
            base.Snapshot();
            revertDrawRectangle = new Rectangle(drawRectangle.X, drawRectangle.Y, drawRectangle.Width, drawRectangle.Height);
        }
        public override void Revert()
        {
            base.Revert();
            drawRectangle = new Rectangle(revertDrawRectangle.X, revertDrawRectangle.Y, revertDrawRectangle.Width, revertDrawRectangle.Height);
        }
    }
}
