/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID AMBIENCE_LOOP_PLAY = 1042302759U;
        static const AkUniqueID AMBIENCE_LOOP_STOP = 4072408949U;
        static const AkUniqueID ENEMY_BOULDER_CAST = 2854117449U;
        static const AkUniqueID ENEMY_BOULDER_IMPACT = 2372917442U;
        static const AkUniqueID ENEMY_DAMAGE = 555067025U;
        static const AkUniqueID ENEMY_DEATH = 1205999388U;
        static const AkUniqueID ENEMY_ERUPTION_CAST = 1789255036U;
        static const AkUniqueID ENEMY_ERUPTION_LAVA = 22559231U;
        static const AkUniqueID ENEMY_MELEE_OVERHEAD_SLAM = 3324595863U;
        static const AkUniqueID ENEMY_MELEE_RIGHT_HOOK = 1491778125U;
        static const AkUniqueID GAME_QUIT = 2356404183U;
        static const AkUniqueID GAME_START = 733168346U;
        static const AkUniqueID MUSIC_DEFEAT_STINGER = 2515303183U;
        static const AkUniqueID MUSIC_GAMEPLAY_LOOP_PLAY = 89443511U;
        static const AkUniqueID MUSIC_GAMEPLAY_LOOP_STOP = 3114429925U;
        static const AkUniqueID MUSIC_VICTORY_STINGER = 2143444826U;
        static const AkUniqueID PLAYER_ATTACK = 2824512041U;
        static const AkUniqueID PLAYER_DAMAGE = 2074073782U;
        static const AkUniqueID PLAYER_DEATH = 3083087645U;
        static const AkUniqueID PLAYER_DODGE = 385316062U;
        static const AkUniqueID PLAYER_JUMP = 1305133589U;
        static const AkUniqueID PLAYER_LAND = 3629196698U;
        static const AkUniqueID PLAYER_SHOOT_CHARGE = 3192392001U;
        static const AkUniqueID PLAYER_SHOOT_FIRE = 190633557U;
        static const AkUniqueID PLAYER_SHOOT_IMPACT = 1650805379U;
        static const AkUniqueID UI_CLICK = 2249769530U;
        static const AkUniqueID UI_HIT_INDICATOR = 396309115U;
        static const AkUniqueID UI_MENU_OFF = 537931389U;
        static const AkUniqueID UI_MENU_ON = 1702827601U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace UI_MENU
        {
            static const AkUniqueID GROUP = 2511555531U;

            namespace STATE
            {
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID UI_MENU_OFF = 537931389U;
                static const AkUniqueID UI_MENU_ON = 1702827601U;
            } // namespace STATE
        } // namespace UI_MENU

    } // namespace STATES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID MUSIC_SIDECHAIN = 266555239U;
        static const AkUniqueID SFX_SIDECHAIN = 2862064063U;
        static const AkUniqueID VICTORY_DEFEAT_SIDECHAIN = 622329408U;
        static const AkUniqueID VOLUME_MASTER = 3695994288U;
        static const AkUniqueID VOLUME_MUSIC = 3891337659U;
        static const AkUniqueID VOLUME_SFX = 3673881719U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID SB_DEFAULT = 822237696U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID AMB_GAMEPLAY_LOOP = 2134809883U;
        static const AkUniqueID AMB_MENU_LOOP = 3762643080U;
        static const AkUniqueID ENEMY_BOULDER_CAST = 2854117449U;
        static const AkUniqueID ENEMY_BOULDER_IMPACT = 2372917442U;
        static const AkUniqueID ENEMY_DAMAGE = 555067025U;
        static const AkUniqueID ENEMY_DEATH = 1205999388U;
        static const AkUniqueID ENEMY_ERUPTION_CAST = 1789255036U;
        static const AkUniqueID ENEMY_ERUPTION_LAVA = 22559231U;
        static const AkUniqueID ENEMY_MELEE_OVERHEAD_SLAM = 3324595863U;
        static const AkUniqueID ENEMY_MELEE_RIGHT_HOOK = 1491778125U;
        static const AkUniqueID MASTER_AMBIENCE = 4104709401U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MASTER_MUSIC = 2318586166U;
        static const AkUniqueID MASTER_SFX = 1078167454U;
        static const AkUniqueID MUSIC_DEFEAT_STINGER = 2515303183U;
        static const AkUniqueID MUSIC_GAMEPLAY_LOOP = 1993100918U;
        static const AkUniqueID MUSIC_VICTORY_STINGER = 2143444826U;
        static const AkUniqueID PLAYER_ATTACK = 2824512041U;
        static const AkUniqueID PLAYER_DAMAGE = 2074073782U;
        static const AkUniqueID PLAYER_DEATH = 3083087645U;
        static const AkUniqueID PLAYER_DODGE = 385316062U;
        static const AkUniqueID PLAYER_JUMP = 1305133589U;
        static const AkUniqueID PLAYER_LAND = 3629196698U;
        static const AkUniqueID PLAYER_SHOOT_CHARGE = 3192392001U;
        static const AkUniqueID PLAYER_SHOOT_FIRE = 190633557U;
        static const AkUniqueID PLAYER_SHOOT_IMPACT = 1650805379U;
        static const AkUniqueID SFX_ENEMY = 2297208207U;
        static const AkUniqueID SFX_PLAYER = 217780010U;
        static const AkUniqueID SFX_UI = 3862737079U;
        static const AkUniqueID UI_CLICK = 2249769530U;
        static const AkUniqueID UI_HIT_INDICATOR = 396309115U;
        static const AkUniqueID UI_MENU_OPEN = 4083126854U;
    } // namespace BUSSES

    namespace AUX_BUSSES
    {
        static const AkUniqueID SFX_REVERB = 3792279061U;
    } // namespace AUX_BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
