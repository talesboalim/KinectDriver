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
using Microsoft.Kinect;

namespace KinDriver
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont sfontPequena, sFontMedia, sFontGrande;

        KinectSensor kinect;

        //Funciona o esqueleto
        Skeleton[] skeletons;
        Skeleton trackedSkeleton1;

        clsObjeto myCar;
        Texture2D myPista;

        //Jogo
        int _Cena = 0;

        //Usado na função Update
        float FootRightZ = 0, FootLeftZ = 0;
        float HandRightY = 0, HandLeftY = 0, AnguloVirar = 0;

        clsObjeto myMenuStick;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 700;

            Content.RootDirectory = "Content";            
        }

        protected override void Initialize()
        {
            //Inicializa o sensor
            try
            {
                kinect = KinectSensor.KinectSensors[0];
                //Inicializa o esqueleto
                kinect.SkeletonStream.Enable();
                //Inicia tudo
                kinect.Start();

                kinect.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(kinect_SkeletonFrameReady);
            }
            catch
            {
            }
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            myCar = new clsObjeto(this, Content.Load<Texture2D>("carro"), new Vector2(graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth), new Vector2(28f, 43f), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, 0f);
            myPista = Content.Load<Texture2D>("Pista");

            myMenuStick = new clsObjeto(this, Content.Load<Texture2D>("MenuStick"), new Vector2(635f, 700f), new Vector2(104f, 162f), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight,0f);

            // Carrega a font para escrita 
            sfontPequena = Content.Load<SpriteFont>("game");
            sFontMedia = Content.Load<SpriteFont>("SpriteFontMedia");
            sFontGrande = Content.Load<SpriteFont>("SpriteFontGrande");
        }

        protected override void OnExiting(Object sender, EventArgs args)
        {
            base.OnExiting(sender, args);

            kinect.Stop();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            float velocidade;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            //verifica se detectou os movimentos
            if (trackedSkeleton1 !=null)
            {
                if (_Cena == 0)
                {
                    //Inicia se a mão direita está acima da cabeça
                    if (trackedSkeleton1.Joints[JointType.HandRight].Position.Y > trackedSkeleton1.Joints[JointType.Head].Position.Y)
                    {
                        _Cena = 1;
                    }
                } else {
                    FootLeftZ = trackedSkeleton1.Joints[JointType.AnkleLeft].Position.Z;
                    FootRightZ = trackedSkeleton1.Joints[JointType.AnkleRight].Position.Z;

                    //acelera o carro de acordo com o pé direito

                    //Ré
                    if ((FootLeftZ - FootRightZ) <= -0.06 && (FootLeftZ - FootRightZ) >= -0.09)
                    {
                        velocidade = (float)-0.5;
                    }
                    else if ((FootLeftZ - FootRightZ) < -0.09 && (FootLeftZ - FootRightZ) >= -0.12)
                    {
                        velocidade = -1;
                    }
                    else if ((FootLeftZ - FootRightZ) < -0.12 && (FootLeftZ - FootRightZ) >= -0.15)
                    {
                        velocidade = (float)-1.5;
                    }
                    else if ((FootLeftZ - FootRightZ) < -0.15)
                    {
                        velocidade = -2;
                    }

                    //Para frente
                    else if ((FootLeftZ - FootRightZ) >= 0.03 && (FootLeftZ - FootRightZ) <= 0.06)
                    {
                        velocidade = velocidade = (float)0.35;
                    }
                    else if ((FootLeftZ - FootRightZ) > 0.06 && (FootLeftZ - FootRightZ) <= 0.09)
                    {
                        velocidade = velocidade = (float)0.75;
                    }
                    else if ((FootLeftZ - FootRightZ) > 0.09 && (FootLeftZ - FootRightZ) <= 0.12)
                    {
                        velocidade = velocidade = (float)1.25;
                    }
                    else if ((FootLeftZ - FootRightZ) > 0.12 && (FootLeftZ - FootRightZ) <= 0.15)
                    {
                        velocidade = velocidade = (float)1.75;
                    }
                    else if ((FootLeftZ - FootRightZ) > 0.15 && (FootLeftZ - FootRightZ) <= 0.18)
                    {
                        velocidade = velocidade = (float)2.25;
                    }
                    else if ((FootLeftZ - FootRightZ) > 0.18 && (FootLeftZ - FootRightZ) <= 0.21)
                    {
                        velocidade = velocidade = (float)2.75;
                    }
                    else if ((FootLeftZ - FootRightZ) > 0.21 && (FootLeftZ - FootRightZ) <= 0.24)
                    {
                        velocidade = velocidade = (float)3.5;
                    }
                    else if ((FootLeftZ - FootRightZ) > 0.24 && (FootLeftZ - FootRightZ) <= 0.27)
                    {
                        velocidade = velocidade = (float)4.8;
                    }
                    else if ((FootLeftZ - FootRightZ) > 0.27)
                    {
                        velocidade = velocidade = (float)6.2;
                    }
                    else
                    {
                        velocidade = 0;
                    }
                    if (velocidade != 0)
                    {
                        
                        //o retorno do kinect é 0.01 Multiplicando por 100 é possível pegar o ângulo
                        //mais fácil
                        //12,96296296296
                        // * 0.00055f
                        HandRightY = ((trackedSkeleton1.Joints[JointType.HandRight].Position.Y) * 10) * 0.00855f;
                        HandLeftY = ((trackedSkeleton1.Joints[JointType.HandLeft].Position.Y) * 10) * 0.00855f;

                        //Vira o carro
                        if (velocidade > 0) //andar para frente
                        {
                            AnguloVirar = HandRightY - HandLeftY; 
                        } else if (velocidade < 0) //andar para trás
                        {
                            AnguloVirar = HandLeftY - HandRightY; 
                        }
                        if (velocidade != 0)
                        {
                            myCar.rotAngle = myCar.rotAngle - AnguloVirar;
                            //ACELERA ou dá a RÉ
                            myCar.MoverFrente(velocidade);
                        }
                        
                    }
                }
                
            } else {
                //se o jogador não foi mais detectado então finaliza o jogo e volta na tela inicial
                jogoIniciar();
            }
            base.Update(gameTime);
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            if (_Cena == 0)//menu
            {
                graphics.GraphicsDevice.Clear(Color.Black);

                spriteBatch.DrawString(sfontPequena, "KinDriver", new Vector2(350, 50), Color.Yellow);

                if (trackedSkeleton1 == null)
                {
                    spriteBatch.DrawString(sFontMedia, "Cadê o motorista?", new Vector2(200, 80), Color.White); 
                }
                else
                {
                    spriteBatch.DrawString(sFontGrande, "Olá motorista!", new Vector2(20, 60), Color.White);

                    spriteBatch.DrawString(sfontPequena, "Deixe os dois pés levemente afastados", new Vector2(150, 200), Color.Turquoise);
                    spriteBatch.DrawString(sfontPequena, "Para dirigir, imagine que você está segurando um volante", new Vector2(150, 230), Color.Turquoise);
                    spriteBatch.DrawString(sfontPequena, "Para acelerar coloque o pé direito direito para frente. Com o pé para trás o carro andará de ré", new Vector2(150, 260), Color.Turquoise);
                    spriteBatch.DrawString(sfontPequena, "Levante a mão direita acima da cabeça para iniciar", new Vector2(150, 290), Color.Turquoise);

                }
            }
            else
            {                
                spriteBatch.Draw(myPista, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);

                myCar.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        void kinect_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    if (skeletons == null)
                    {
                        skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    }

                    skeletonFrame.CopySkeletonDataTo(skeletons);

                    trackedSkeleton1 = skeletons.Where(s => s.TrackingState == SkeletonTrackingState.Tracked).FirstOrDefault();
                }
            }
        }

        void jogoIniciar()
        {
            _Cena = 0;
            myCar.position=new Vector2(800.0f, 1300.0f);
        }
    }

    internal static class ExtensionMethods
    {
        public static Joint Scale(this Joint joint, int width, int height)
        {
            SkeletonPoint skeletonPoint = new SkeletonPoint()
            {
                X = Scale(joint.Position.X, width),
                Y = Scale(-joint.Position.Y, height),
                Z = joint.Position.Z
            };

            Joint scaledJoint = new Joint()
            {
                TrackingState = joint.TrackingState,
                Position = skeletonPoint
            };

            return scaledJoint;
        }

        public static float Scale(float value, int max)
        {
            return (max >> 1) + (value * (max >> 1));
        }
    }
}
