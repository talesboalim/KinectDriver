using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace KinDriver
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class clsObjeto : Microsoft.Xna.Framework.GameComponent
    {
        public Texture2D texture { get; set; }//textura da sprite
        public Vector2 position { get; set; }//posição da sprite na tela
        public Vector2 size { get; set; }//tamanho da sprite na tela
        public float rotAngle;

        private Vector2 screenSize { get; set; }

        public clsObjeto(Game game, Texture2D newTexture, Vector2 newPosition, Vector2 newSize, int ScreenWidth, int ScreenHeight, float RotationAngle)
            : base(game)
        {
            // TODO: Construct any child components here
            texture = newTexture;
            position = newPosition;
            size = newSize;
            screenSize = new Vector2(ScreenWidth, ScreenHeight);
            rotAngle= RotationAngle;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            float circle = MathHelper.Pi * 2;
            rotAngle  = rotAngle % circle;

            spriteBatch.Draw(texture, position /2 , null, Color.White, rotAngle, size/2, 1.0f, SpriteEffects.None, 0f);
        }

        public void MoverFrente(float velocidade)
        {
            Vector2 mover = new Vector2();

            mover.X = velocidade * (float)Math.Sin(rotAngle);
            mover.Y = velocidade * (float)-Math.Cos(rotAngle);

            position = new Vector2(position.X + mover.X, position.Y + mover.Y);
        }
    }
}
