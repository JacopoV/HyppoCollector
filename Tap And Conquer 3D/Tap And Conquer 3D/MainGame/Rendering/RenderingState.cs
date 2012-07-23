using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;


namespace HyppoCollector
{
    public class RenderingState : RenderingAnimal
  {
    public Texture2D glowingLine;
    public Texture2D radarScanLine;
    public Texture2D radarScanCircle;
    public SpriteFont font;

    public Texture2D life, score ,fartingBar, fartingButton;


    public RenderingState(ContentManager Content) : base(Content)
    {
      glowingLine = Content.Load<Texture2D>("Textures/glowingLine");
      radarScanCircle = Content.Load<Texture2D>("Textures/radarScanCircle");
      radarScanLine = Content.Load<Texture2D>("Textures/radarScanLine");
      font = Content.Load<SpriteFont>("Fonts/Regular24");
      life = content.Load<Texture2D>("Textures/life3");
      score = Content.Load<Texture2D>("Textures/score");
      fartingBar = Content.Load<Texture2D>("Textures/bar/bar0");
      fartingButton = Content.Load<Texture2D>("Textures/fart_button_alpha");
     
    }

    

    public void DrawCentered(SpriteBatch spriteBatch, string text, Vector2 position, Color color)
    {
      var textSize = font.MeasureString(text);
      spriteBatch.DrawString(font, text, position - textSize * 0.5f + Vector2.One * 2.0f, Color.Black);
      spriteBatch.DrawString(font, text, position - textSize * 0.5f, color);
    }
   
    public void Draw(SpriteBatch spriteBatch, string text, Vector2 position, Color color)
    {
      spriteBatch.DrawString(font, text, position + Vector2.One * 2.0f, Color.Black);
      spriteBatch.DrawString(font, text, position, color);
    }
    
    public void DrawRightAligned(SpriteBatch spriteBatch, string text, Vector2 position, Color color)
    {
      var textSize = font.MeasureString(text);
      textSize.Y = 0.0f;
      spriteBatch.DrawString(font, text, position - textSize + Vector2.One * 2.0f, Color.Black);
      spriteBatch.DrawString(font, text, position - textSize, color);
    }
    
    public void DrawTooltipOpening(SpriteBatch spriteBatch, string text, Vector2 position, Color color, float tooltipTimer)
    {
      tooltipTimer = MathHelper.SmoothStep(0.0f, 1.0f, tooltipTimer);
      position -= 480 * Vector2.UnitY * (1.0f - tooltipTimer);
      var textSize = font.MeasureString(text);
      spriteBatch.DrawString(font, text, position + Vector2.One * 2.0f, Color.Black,
        0.0f, textSize * 0.5f, tooltipTimer, SpriteEffects.None, 0);
      spriteBatch.DrawString(font, text, position, color,
        0.0f, textSize * 0.5f, tooltipTimer, SpriteEffects.None, 0);
    }

    public void DrawTooltipClosing(SpriteBatch spriteBatch, string text, Vector2 position, Color color, float tooltipTimer)
    {
      tooltipTimer = MathHelper.SmoothStep(0.0f, 1.0f, tooltipTimer);
      position -= 480 * Vector2.UnitY * (1.0f - tooltipTimer);
      var textSize = font.MeasureString(text);
      spriteBatch.DrawString(font, text, position + Vector2.One * 2.0f, Color.Black,
        0.0f, textSize * 0.5f, tooltipTimer, SpriteEffects.None, 0);
      spriteBatch.DrawString(font, text, position, color,
        0.0f, textSize * 0.5f, tooltipTimer, SpriteEffects.None, 0);
    }


    public void Draw()
    {

        base.Draw();

        GameState gs = (GameState)content.ServiceProvider.GetService(typeof(GameState));

        switch (gs.player.life)
        {
            case 2 :
                life = content.Load<Texture2D>("Textures/life2");
                break;
            case 1 :
                life = content.Load<Texture2D>("Textures/life1");
                break;
            case 0 :
                life = null;
                break;
        }

        switch (gs.fartingBarStep)
        {

            case 0:
                fartingBar = content.Load<Texture2D>("Textures/bar/bar0");
                break;
            case 10:
                fartingBar = content.Load<Texture2D>("Textures/bar/bar10");
                break;
            case 20:
                fartingBar = content.Load<Texture2D>("Textures/bar/bar20");
                break;
            case 30:
                fartingBar = content.Load<Texture2D>("Textures/bar/bar30");
                break;
            case 40:
                fartingBar = content.Load<Texture2D>("Textures/bar/bar40");
                break;
            case 50:
                fartingBar = content.Load<Texture2D>("Textures/bar/bar50");
                break;
            case 60:
                fartingBar = content.Load<Texture2D>("Textures/bar/bar60");
                break;
            case 70:
                fartingBar = content.Load<Texture2D>("Textures/bar/bar70");
                break;
            case 80:
                fartingBar = content.Load<Texture2D>("Textures/bar/bar80");
                break;
            case 90:
                fartingBar = content.Load<Texture2D>("Textures/bar/bar90");
                break;
            case 100:
                fartingBar = content.Load<Texture2D>("Textures/bar/bar100");
                break;


        }

        spriteBatch.Draw(fartingBar, new Rectangle(250,445,300,20), Color.White);


        if (gs.fartingBarStep == gs.fartingBarMax)
            fartingButton = content.Load<Texture2D>("Textures/fart_button");
        else
            fartingButton = content.Load<Texture2D>("Textures/fart_button_alpha");

        spriteBatch.Draw(fartingButton, new Rectangle(5, 5, 80, 80), Color.White);


        if(life!=null)
        {
            spriteBatch.Draw(life, new Rectangle(700,435,100,40),Color.White);
        }

        spriteBatch.Draw(score, new Rectangle(5,435,100,40), Color.White);
        spriteBatch.DrawString(font,gs.totalPoints.ToString(), new Vector2(110, 435), Color.White);



        
    }

  }

}
