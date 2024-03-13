package com.github.fantasy0v0.netty;

import io.netty.buffer.ByteBuf;
import io.netty.buffer.ByteBufUtil;
import io.netty.buffer.Unpooled;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.SimpleChannelInboundHandler;
import io.netty.util.CharsetUtil;

import java.nio.charset.StandardCharsets;

public class DiscardServerHandler extends SimpleChannelInboundHandler<Long> {

  @Override
  protected void channelRead0(ChannelHandlerContext ctx, Long msg) throws Exception {
    String tmp = "Hello " + msg;
    new Thread(() -> {
      try {
        Thread.sleep(300);
        ctx.writeAndFlush(tmp);
      } catch (InterruptedException e) {
        throw new RuntimeException(e);
      }
    }).start();
  }
}
